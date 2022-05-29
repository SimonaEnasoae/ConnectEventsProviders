using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBff.Controllers.Responses.Providers
{
    public class ProviderPaginationResponse
    {
        public int TotalProviders { get; set; }

        public int TotalPages { get; set; }

        public ICollection<ProviderModel> Data { get; set; }
    }
}
