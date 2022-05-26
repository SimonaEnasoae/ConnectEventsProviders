using GrpcRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBff.Controllers.Responses
{
    public class ExtendedRequestEvent : RequestEvent
    {
        public ExtendedRequestEvent(RequestEventResponse request) : base(request)
        {

        }

        public EventModel EventModel { get; set; }
        public Provider Provider { get; internal set; }
    }
}
