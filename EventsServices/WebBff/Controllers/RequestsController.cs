using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Requests;
using WebBff.Controllers.Responses;
using WebBff.Model;
using WebBff.Services;

namespace WebBff.Controllers
{
    [Route("api/requests")]
    [ApiController]
    public class RequestsController : ControllerBase
    {

        private readonly IRequestService _requestService;
        private readonly IEventService _eventService;
        private readonly IProviderService _providerService;

        public RequestsController(IRequestService requestService, IEventService eventService, IProviderService providerService)
        {
            _requestService = requestService;
            _eventService = eventService;
            _providerService = providerService;
        }

        [HttpPost]
        public async Task<CreateRequestResponse> Post([FromBody] CreateRequest request)
        {
            var response = await _requestService.CreateAsync(new RequestData(request));
            if (!String.IsNullOrEmpty(response.Id))
            {
                return new CreateRequestResponse() { success = true };
            }
            else
            {
                return new CreateRequestResponse() { success = false };

            }
        }

        [HttpGet]
        public async Task<RequestEventOrganiserPaginationRespose> GetRequestAsync([FromQuery] string userId = "", int pageSize = 6, int pageIndex = 0)
        {
            var response = await _requestService.GetRequestEventsByOrganiserIdAsync(userId, pageSize, pageIndex);
            var pagination = new RequestEventOrganiserPaginationRespose(response);
            foreach (var res in pagination.Data)
            {

                var eventObj = await _eventService.GetEventByIdAsync(res.EventId);
                res.EventModel = eventObj;

                var provider = await _providerService.GetProviderByIdAsync(res.SenderId);
                res.Provider = provider;
            }
            return pagination;
        }

        [Route("status")]
        [HttpPost]
        public async Task<CreateRequestResponse> PostRequestStatusAsync([FromBody] UpdateRequest request)
        {
            var response = await  _requestService.UpdateStatus(request.Id, request.Status, request.Token);
            return new CreateRequestResponse() { success = true };
        }
    }
}
