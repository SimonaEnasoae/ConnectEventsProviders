using GrpcEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBff.Controllers.Responses.Events
{
    public class EventModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string OrganiserId { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public IEnumerable<string>  Tags { get; init; }

        public Byte[] Image { get; set; }
        public string Token { get; set; }


        public EventModel(EventResponse eventObj)
        {
            Id = eventObj.Id;
            Title = eventObj.Title;
            Description = eventObj.Description;
            Location = eventObj.Location;
            OrganiserId = eventObj.OrganiserId;
            EndDate = eventObj.EndDate.ToDateTime();
            StartDate = eventObj.StartDate.ToDateTime();
            Image = eventObj.Image.ToByteArray();
            Tags = eventObj.Tags.Select(tag => tag.Value).ToList();
        }

        public EventModel(){}
    }
}
