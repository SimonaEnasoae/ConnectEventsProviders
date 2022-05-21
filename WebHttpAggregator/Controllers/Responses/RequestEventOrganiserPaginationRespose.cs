using GrpcRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBff.Controllers.Responses
{
    public class RequestEventOrganiserPaginationRespose
    {
        public RequestEventOrganiserPaginationRespose(PaginatedRequestEventsResponse response)
        {
            TotalRequests = response.Data.Count;
            TotalPages = response.Data.Count / response.PageSize + 1;
            Data = new List<ComplexRequestEvent>();
            foreach(RequestEventResponse req in response.Data)
            {
                Data.Add(new ComplexRequestEvent(req));
            }
        }

        public int TotalRequests { get; set; }

        public int TotalPages { get; set; }

        public ICollection<ComplexRequestEvent> Data { get; set; }
    }
}
