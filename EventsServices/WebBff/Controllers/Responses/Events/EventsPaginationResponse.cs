using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBff.Controllers.Responses.Events
{
    public class EventsPaginationResponse
    {
        public int TotalEvents { get; set; }

        public int TotalPages { get; set; }

        public ICollection<EventModel> Data { get; set; }
    }
}
