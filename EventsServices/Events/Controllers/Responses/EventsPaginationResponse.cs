using Events.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Controllers.Responses
{
    public class EventsPaginationResponse
    {
        public int TotalEvents { get; set; }

        public int TotalPages { get; set; }

        public ICollection<EventResponse> Data { get; set; }
    }
}
