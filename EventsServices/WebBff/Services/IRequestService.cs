using GrpcRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Model;

namespace WebBff.Services
{
    public interface IRequestService
    {
        Task<RequestData> CreateAsync(RequestData requestData);

        Task<PaginatedRequestEventsResponse> GetRequestEventsByOrganiserIdAsync(string organiserId, int pageSize, int pageIndex);
        Task<RequestData> UpdateStatus(string id, int status);
    }
}
