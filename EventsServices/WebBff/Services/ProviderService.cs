using GrpcProvider;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Responses;

namespace WebBff.Services
{
    public class ProviderService : IProviderService
    {
        private readonly GrpcProvider.Provider.ProviderClient _providerClient;

        public ProviderService(GrpcProvider.Provider.ProviderClient providerClient)
        {
            _providerClient = providerClient;
        }

        public async Task<Controllers.Responses.Provider> GetProviderByIdAsync(string id)
        {
            var provider = await _providerClient.GetProviderByIdAsync(new ProviderRequest() { Id = id });
            return new Controllers.Responses.Provider(provider);
        }
    }
}
