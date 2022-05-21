using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Controllers.Responses
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

        public IEnumerable<string> Tags { get; set; }

    }
}
