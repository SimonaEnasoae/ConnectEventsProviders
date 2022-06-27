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

            var requestResponse = new RequestEventReq()
            {
                Id = "",
                Senderid = requestData.SenderId,
                Receiverid = requestData.ReceiverId,
                Eventid = requestData.EventId,
                Status = RequestEventReq.Types.Status.Pending,
                Token = requestData.Token
            };
            var response = await _requestClient.CreateRequestEventAsync(requestResponse);
            //_logger.LogDebug("grpc client created, response = {@response}", response);

            return new RequestData(response);
        }


        public async Task<PaginatedRequestEventsResponse> GetRequestEventsByOrganiserIdAsync(string userId, int pageSize, int pageIndex)
        {
            return await _requestClient.GetRequestEventsByUserIdAsync(new RequestEventsByUserId()
            {
                UserId = userId,
                PageSize = pageSize,
                PageIndex = pageIndex
            });

        }

        public async Task<RequestData> UpdateStatus(string id, int status, string token)
        {
            var request = new UpdateRequestEventRequest()
            {
                Id = id,
                Status = (UpdateRequestEventRequest.Types.Status)status,
                Token = token
            };
            var response = await _requestClient.UpdateRequestEventAsync(request);
            return new RequestData(response);
        }
    }
}
