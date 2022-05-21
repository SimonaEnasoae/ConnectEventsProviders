using Grpc.Core;
using GrpcRequest;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebBff.Model;


namespace WebBff.Services
{
    public class RequestService : IRequestService
    {
        private readonly Request.RequestClient _requestClient;
        private readonly ILogger<RequestService> _logger;

        public RequestService(Request.RequestClient requestClient, ILogger<RequestService> logger)
        {
            _requestClient = requestClient;
            _logger = logger;
        }

        public async Task<RequestData> CreateAsync(RequestData requestData)
        {
            //_logger.LogDebug("grpc client created, request = {@requestData}", requestData);

            var requestResponse = new RequestEventResponse
            {
                Id = "",
                Senderid = requestData.SenderId,
                Receiverid = requestData.ReceiverId,
                Eventid = requestData.EventId,
                Status = RequestEventResponse.Types.Status.Pending
            };
            var response = await _requestClient.CreateRequestEventAsync(requestResponse);
            //_logger.LogDebug("grpc client created, response = {@response}", response);

            return new RequestData(response);
        }

        public Task<RequestData> UpdateAsync(RequestData currentBasket)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedRequestEventsResponse> GetRequestEventsByOrganiserIdAsync(string organiserId, int pageSize, int pageIndex)
        {
            return await _requestClient.GetRequestEventsByOrganiserIdAsync(new RequestEventsByOrganiserId()
            {
                OrganiserId = organiserId,
                PageSize = pageSize,
                PageIndex = pageIndex
            });

        }
    }
}
