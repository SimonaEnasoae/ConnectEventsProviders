using Events.Persistence;
using Google.Protobuf;
using Grpc.Core;
using GrpcEvent;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Grpc
{
    public class EventService : Event.EventBase
    {
        private readonly EventDbContext eventDbContext;

        public EventService(EventDbContext context)
        {
            eventDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        [AllowAnonymous]
        public override Task<EventResponse> GetEventById(EventRequest request, ServerCallContext context)
        {
            var id = request.Id;

            var eventObject = eventDbContext.GetEventById(id);

            var response=  new EventResponse() {
                Id = eventObject.Id,
                Title = eventObject.Title,
                Description = eventObject.Description,
                Location = eventObject.Location,
                StartDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind(eventObject.StartDate, DateTimeKind.Utc)),
                EndDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind(eventObject.StartDate, DateTimeKind.Utc)),
                Image = ByteString.CopyFrom(System.IO. File.ReadAllBytes(eventObject.PictureUri)),

        };
            response.Tags.Add(eventObject.EventTags.Select(eventTag => new Tag() { Id = eventTag.Tag.Id, Value = eventTag.Tag.Value }).ToList());
            return Task.FromResult(response);
        }
    }
}
