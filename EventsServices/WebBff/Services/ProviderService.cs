using Google.Protobuf;
using GrpcEvent;
using GrpcProvider;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Responses;
using WebBff.Controllers.Responses.Providers;

namespace WebBff.Services
{
    public class ProviderService : IProviderService
    {
        private readonly GrpcProvider.Provider.ProviderClient _providerClient;

        public ProviderService(GrpcProvider.Provider.ProviderClient providerClient)
        {
            _providerClient = providerClient;
        }

        public async Task<ProviderModel> GetProviderByIdAsync(string id)
        {
            var provider = await _providerClient.GetProviderByIdAsync(new ProviderRequest() { Id = id });
            return new ProviderModel(provider);
        }

        async public Task<ProviderModel> UpdateProvider(ProviderModel provider)
        {
            var request = new ProviderResponse() {
                Id = provider.Id,
                Title = provider.Title,
                Description = provider.Description,
                Location = provider.Location,
                Tag = provider.Tag,
            };
           
            var eventObj = await _providerClient.UpdateProviderAsync(request);


            return new ProviderModel(eventObj);
        }

        async public Task<bool> UpdatePicture(FileModel file)
        {
            using (var ms = new MemoryStream())
            {
                file.FormFile.CopyTo(ms);

                var response = await _providerClient.UpdatePictureAsync(new ProviderPictureRequest() {
                    Id = file.ProviderId,
                    FileName = file.FileName,
                    Image = ByteString.CopyFrom(ms.ToArray())
                });
                return response.Status;
            }
        }

        public Task<ProviderPaginationResponse> GetProvidersAsync(int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }
    }
}
