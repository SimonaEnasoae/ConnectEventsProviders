using Google.Protobuf;
using Grpc.Core;
using GrpcProvider;
using Microsoft.AspNetCore.Authorization;
using Providers.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Providers.Grpc
{
    public class ProviderService : Provider.ProviderBase
    {
        private readonly ProviderDbContext providerDbContext;

        public ProviderService(ProviderDbContext context)
        {
            providerDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        [AllowAnonymous]
        public override Task<ProviderResponse> GetProviderById(ProviderRequest request, ServerCallContext context)
        {
            var id = request.Id;

            var eventObject = providerDbContext.GetProvider(id);

            var response = new ProviderResponse() {
                Id = eventObject.Id,
                Title = eventObject.Title,
                Description = eventObject.Description,
                Location = eventObject.Location,
                Image = ByteString.CopyFrom(System.IO.File.ReadAllBytes(eventObject.PictureUri)),
                Tag = eventObject.Tag.Value

            };
            return Task.FromResult(response);
        }
    }
}
