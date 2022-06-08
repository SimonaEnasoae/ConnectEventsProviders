using Events.Controllers.Responses;
using Events.Model;
using Events.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;

namespace Events.Controllers
{
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly EventDbContext _eventDbContext;
        public EventController(EventDbContext context)
        {
            _eventDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public EventsPaginationResponse GetAsync([FromQuery] string organiserId ="", int pageSize = 9, [FromQuery] int pageIndex = 0)
        {
            var events = Enumerable.Empty<EventDb>();
            if (organiserId == "" || organiserId == null)
            {
                events = _eventDbContext.GetAll();
            }
            else
            {
                events = _eventDbContext.GetAllByOrganiser(organiserId);

            }

            var eventsOnpage =events
           .Skip(pageSize * pageIndex)
           .Take(pageSize);

            var eventResponses = new List<EventResponse>();
            foreach (EventDb currentEvent in eventsOnpage)
            {
                EventResponse eventResponse = new EventResponse(currentEvent);
                eventResponse.Image = System.IO.File.ReadAllBytes(currentEvent.PictureUri);
                eventResponses.Add(eventResponse);
            }
            EventsPaginationResponse eventPagination = new EventsPaginationResponse() {
                TotalEvents = 17,
                TotalPages = 2,
                Data = eventResponses
            };
            return eventPagination;
        }

        [HttpPost]
        public EventResponse Post([FromBody]EventModel eventModel)
        {
            
            var id = Guid.NewGuid().ToString();
            List<EventTag> eventTags = new List<EventTag>();
            foreach (string tag in eventModel.Tags)
            {
                Tag dbTag = _eventDbContext.Tags.Where(currentTag => currentTag.Value == tag).FirstOrDefault();
                eventTags.Add(new EventTag() { EventId = id, TagId = dbTag.Id });
            }

            var newEvent = new EventDb() {
                Id = id,
                Title = eventModel.Title,
                Description = eventModel.Description,
                EventTags = eventTags,
                Location = eventModel.Location,
                StartDate = eventModel.StartDate,
                EndDate = eventModel.EndDate,
                OrganiserId = eventModel.OrganiserId,
            };

            EventResponse eventResponse = new EventResponse(newEvent);
            _eventDbContext.Events.Add(newEvent);
            _eventDbContext.SaveChanges();

            return eventResponse;
        }

        [Route("file")]
        [HttpPost]
        public bool PostFile([FromForm] FileModel file)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);
            using (Stream stream =new FileStream(path, FileMode.Create))
            {
                EventDb dbEvent = _eventDbContext.Events.Where(eventModel => eventModel.Id == file.EventId).FirstOrDefault();
                dbEvent.PictureUri = path;
                _eventDbContext.Update(dbEvent);
                _eventDbContext.SaveChanges();
                file.FormFile.CopyTo(stream);
            }
            return true;
        }
    }
}
