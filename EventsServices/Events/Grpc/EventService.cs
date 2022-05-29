using Events.Controllers.Responses;
using Events.Model;
using Events.Persistence;
using Google.Protobuf;
using Grpc.Core;
using GrpcEvent;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventResponse = GrpcEvent.EventResponse;
using Tag = GrpcEvent.Tag;

namespace Events.Grpc
{
    public class EventService : Event.EventBase
    {
        private readonly EventDbContext _eventDbContext;

        public EventService(EventDbContext context)
        {
            _eventDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        [AllowAnonymous]
        public override Task<EventResponse> GetEventById(EventRequest request, ServerCallContext context)
        {
            var id = request.Id;

            var eventObject = _eventDbContext.GetEventById(id);

            var response = new EventResponse() {
                Id = eventObject.Id,
                Title = eventObject.Title,
                Description = eventObject.Description,
                Location = eventObject.Location,
                StartDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                    DateTime.SpecifyKind(eventObject.StartDate, DateTimeKind.Utc)),
                EndDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                    DateTime.SpecifyKind(eventObject.StartDate, DateTimeKind.Utc)),
                Image = ByteString.CopyFrom(System.IO.File.ReadAllBytes(eventObject.PictureUri)),

            };
            response.Tags.Add(eventObject.EventTags
                .Select(eventTag => new Tag() {Id = eventTag.Tag.Id, Value = eventTag.Tag.Value}).ToList());
            return Task.FromResult(response);
        }

        [AllowAnonymous]
        override public Task<EventPagination> GetEvents(EventsRequest request, ServerCallContext context)
        {
            var events = Enumerable.Empty<EventDb>();
            if (request.OrganiserId == "" || request.OrganiserId == null)
            {
                events = _eventDbContext.GetAll();
            }
            else
            {
                events = _eventDbContext.GetAllByOrganiser(request.OrganiserId);

            }

            var eventsOnPage = events
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);

            EventPagination eventPagination = new() {
                TotalEvents = events.Count(),
                TotalPages = events.Count() / request.PageSize + 1,
            };
            foreach (var eventObject in eventsOnPage)
            {
                var eventResponse = new EventResponse() {
                    Id = eventObject.Id,
                    Title = eventObject.Title,
                    Description = eventObject.Description,
                    Location = eventObject.Location,
                    StartDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                        DateTime.SpecifyKind(eventObject.StartDate, DateTimeKind.Utc)),
                    EndDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                        DateTime.SpecifyKind(eventObject.StartDate, DateTimeKind.Utc)),
                    Image = ByteString.CopyFrom(System.IO.File.ReadAllBytes(eventObject.PictureUri)),
                };

                eventPagination.Data.Add(eventResponse);
            }

            return Task.FromResult(eventPagination);

        }

        [AllowAnonymous]
        override public Task<EventResponse> SaveEvent(EventResponse eventObject, ServerCallContext context)
        {
            var eventTags = new List<EventTag>();
            var id = Guid.NewGuid().ToString();

            foreach (Tag tag in eventObject.Tags)
            {
                var tagDb = _eventDbContext.Tags.Where(tagDb => tagDb.Value == tag.Value).FirstOrDefault();
                eventTags.Add(new EventTag() {EventId = id, TagId = tagDb.Id});
            }

            var eventDb = new EventDb() {
                Id = id,
                Title = eventObject.Title,
                Description = eventObject.Description,
                Location = eventObject.Location,
                StartDate = eventObject.StartDate.ToDateTime(),
                EndDate = eventObject.EndDate.ToDateTime(),
                EventTags = eventTags
            };
            _eventDbContext.Events.Add(eventDb);
            _eventDbContext.SaveChanges();

            eventObject.Id = id;
            eventObject.Image = ByteString.Empty;
            return Task.FromResult(eventObject);
        }

        [AllowAnonymous]
        override public Task<EventPictureResponse> SavePicture(EventPictureRequest request, ServerCallContext context)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", request.FileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                EventDb dbEvent = _eventDbContext.Events.Where(eventModel => eventModel.Id == request.Id).FirstOrDefault();
                dbEvent.PictureUri = path;
                _eventDbContext.Update(dbEvent);
                _eventDbContext.SaveChanges();
                stream.Write(request.Image.ToByteArray());
                return Task.FromResult(new EventPictureResponse(){Status = true});
            }
        }
    }
}
