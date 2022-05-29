using Events.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Controllers.Responses
{
    public class EventResponse 
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Location { get; init; }
        public DateTime StartDate { get; init; }

        public DateTime EndDate { get; init; }
         public IEnumerable<TagDto> Tags { get; init; }

        public Byte[] Image { get; set; }
        public string OrganiserId { get; init; }

        public EventResponse(EventDb baseEvent)
        {
            this.Id = baseEvent.Id;
            this.Title = baseEvent.Title;
            this.Location = baseEvent.Location;
            this.StartDate = baseEvent.StartDate;
            this.EndDate = baseEvent.EndDate;
            this.Tags = baseEvent.EventTags.Select(eventTag => new TagDto(){ Id = eventTag.Tag.Id, Value = eventTag.Tag.Value});
            this.Description = baseEvent.Description;
            this.OrganiserId = baseEvent.OrganiserId;
        }

       
    }
}
