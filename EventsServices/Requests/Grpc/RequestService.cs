using Grpc.Core;
using GrpcAuth;
using GrpcRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Requests.Model;
using Requests.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Grpc
{
    public class RequestService : Request.RequestBase
    {
        private readonly RequestsDbContext requestsDbContext;
        private readonly Auth.AuthClient _authClient;


        public RequestService(RequestsDbContext context, Auth.AuthClient authClient)
        {
            _authClient = authClient;
            requestsDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        [AllowAnonymous]
        public override async Task<RequestEventResponse> CreateRequestEvent(RequestEventReq request, ServerCallContext context)
        {
            var id = Guid.NewGuid().ToString();
            request.Id = id;

            var checkActionResponse = await _authClient.CheckActionAsync(new CheckActionRequest() {
                Id = "id",
                Token = request.Token
            });

            if (checkActionResponse.Status)
            {
                await requestsDbContext.RequestEvents.AddAsync(new RequestEvent(request));
                requestsDbContext.SaveChanges();

                var response = new RequestEventResponse() {
                    Id = id,
                    Receiverid = request.Receiverid,
                    Senderid = request.Senderid,
                    Eventid = request.Eventid,
                    Status = (RequestEventResponse.Types.Status)request.Status,
                };
                return response;
            }

            return new RequestEventResponse();

        }

        [AllowAnonymous]
        public override async Task<RequestEventResponse> UpdateRequestEvent(UpdateRequestEventRequest request, ServerCallContext context)
        {
            var checkActionResponse = await _authClient.CheckActionAsync(new CheckActionRequest() {
                Id = "id",
                Token = request.Token
            });

            if (checkActionResponse.Status)
            {

                var req = await requestsDbContext.RequestEvents.Where(req => request.Id.Equals(req.Id)).FirstOrDefaultAsync();
                req.Status = (Model.Status)request.Status;
                requestsDbContext.SaveChanges();

                return new RequestEventResponse() {
                    Id = request.Id,
                    Senderid=req.SenderId,
                    Receiverid=req.SenderId,
                    Eventid=req.EventId,
                    Status= (RequestEventResponse.Types.Status)req.Status};
            }

            return new RequestEventResponse();
        }

        [AllowAnonymous]
        public override async Task<PaginatedRequestEventsResponse> GetRequestEventsByUserId(RequestEventsByUserId request, ServerCallContext context)
        {
            var requests = await requestsDbContext.RequestEvents.Where(req => request.UserId.Equals(req.SenderId)||request.UserId.Equals(req.ReceiverId)).ToListAsync();

            var requestsOnPage = requests
          .Skip(request.PageSize * request.PageIndex)
          .Take(request.PageSize).ToList();

            var result = new PaginatedRequestEventsResponse() {
                Count = requests.Count,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            requestsOnPage.ForEach(request => {
                result.Data.Add(new RequestEventResponse() {
                    Id = request.Id,
                    Senderid  =request.SenderId,
                    Receiverid = request.SenderId,
                    Eventid = request.EventId,
                    Status = (RequestEventResponse.Types.Status)request.Status
                }); ; }) ;

            return result;

        }

    }
}
