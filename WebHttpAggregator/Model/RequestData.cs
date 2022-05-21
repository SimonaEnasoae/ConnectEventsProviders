using GrpcRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Requests;

namespace WebBff.Model
{
    public class RequestData
    {

        public RequestData(RequestEventResponse response)
        {
            Id = response.Id;
            SenderId = response.Senderid;
            ReceiverId = response.Receiverid;
            EventId = response.Eventid;
            Status = response.Status.ToString();
        }

        public RequestData(CreateRequest request)
        {
            Id = request.Id;
            SenderId = request.SenderId;
            ReceiverId = request.ReceiverId;
            EventId = request.EventId;
        }

        public string Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string EventId { get; set; }
        public string Status { get; set; }
    }
}
