using GrpcRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBff.Controllers.Responses
{
    public class ComplexRequestEvent : RequestEvent
    {
        public ComplexRequestEvent(RequestEventResponse request) : base(request)
        {
        }

        public EventModel EventModel { get; set; }
        public Provider Provider { get; internal set; }
    }
}
