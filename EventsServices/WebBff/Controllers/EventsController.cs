using GrpcEvent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Responses.Events;
using WebBff.Services;

namespace WebBff.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        async public Task<EventsPaginationResponse> GetAsync([FromQuery] string organiserId = "", int pageSize = 9, int pageIndex = 0)
        {
           
            return await _eventService.GetEventsAsync(organiserId,pageSize,pageIndex);
        }


        [HttpPost]
        public async Task<EventModel> PostAsync([FromBody] EventModel eventModel)
        {
            return await _eventService.SaveEvent(eventModel);
        }

        [Route("file")]
        [HttpPost]
        async public Task<bool> PostFile([FromForm] FileModel file)
        {
            return await _eventService.SavePicture(file);
        }
    }
}
