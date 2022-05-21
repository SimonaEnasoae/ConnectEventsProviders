using Grpc.Core;
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

        public RequestService(RequestsDbContext context)
        {
            requestsDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        [AllowAnonymous]
        public override async Task<RequestEventResponse> CreateRequestEvent(RequestEventResponse request, ServerCallContext context)
        {
            var id = Guid.NewGuid().ToString();
            request.Id = id;
            await requestsDbContext.RequestEvents.AddAsync(new RequestEvent(request));
            requestsDbContext.SaveChanges();
           
            return request;
        }

        [AllowAnonymous]
        public override async Task<RequestEventResponse> UpdateRequestEvent(RequestEventResponse request, ServerCallContext context)
        {
            return new RequestEventResponse() { Id = "1" };
        }

        [AllowAnonymous]
        public override async Task<PaginatedRequestEventsResponse> GetRequestEventsByOrganiserId(RequestEventsByOrganiserId request, ServerCallContext context)
        {
            var requests = await requestsDbContext.RequestEvents.Where(req => request.OrganiserId.Equals(req.SenderId)).ToListAsync();

            var requestsOnpage = requests
          .Skip(request.PageSize * request.PageIndex)
          .Take(request.PageSize);

            var result = new PaginatedRequestEventsResponse() {
                Count = requests.Count,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            requests.ForEach(request => {
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
