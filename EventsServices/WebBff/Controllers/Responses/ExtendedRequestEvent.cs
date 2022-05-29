using GrpcRequest;
using WebBff.Controllers.Responses.Events;
using WebBff.Controllers.Responses.Providers;

namespace WebBff.Controllers.Responses
{
    public class ExtendedRequestEvent : RequestEvent
    {
        public ExtendedRequestEvent(RequestEventResponse request) : base(request)
        {

        }

        public EventModel EventModel { get; set; }
        public ProviderModel Provider { get; internal set; }
    }
}
