using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Providers.Controllers.Responses
{
    public class ProviderPaginationResponse
    {
        public int TotalProviders { get; set; }

        public int TotalPages { get; set; }

        public ICollection<ProviderResponse> Data { get; set; }
    }
}
