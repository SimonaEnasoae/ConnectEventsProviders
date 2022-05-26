using GrpcEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Responses;

namespace WebBff.Services
{
    public class EventService : IEventService
    {
        private readonly Event.EventClient _eventClient;

        public EventService(Event.EventClient eventClient)
        {
            _eventClient = eventClient;
        }
        public async Task<EventModel> GetEventByIdAsync(string id)
        {
            var eventObj = await _eventClient.GetEventByIdAsync(new EventRequest() { Id = id});
            return new EventModel(eventObj);

        }
    }
}
