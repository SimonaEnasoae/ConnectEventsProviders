using GrpcRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBff.Controllers.Responses
{
    public enum Status
    {
        Pending,
        Accepeted,
        Declined
    }
    public class RequestEvent
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string EventId { get; set; }

        public Status Status { get; set; }

        public RequestEvent() { }

        public RequestEvent(RequestEventResponse request)
        {
            Id = request.Id;
            ReceiverId = request.Receiverid;
            SenderId = request.Senderid;
            EventId = request.Eventid;
            Status = (Status)request.Status;
        }

    }
}
