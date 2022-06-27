using Google.Protobuf;
using Grpc.Core;
using GrpcAuth;
using GrpcProvider;
using Microsoft.AspNetCore.Authorization;
using Providers.Models;
using Providers.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Providers.Grpc
{
    public class ProviderService : Provider.ProviderBase
    {
        private readonly ProviderDbContext _providerDbContext;
        private readonly Auth.AuthClient _authClient;

        public ProviderService(ProviderDbContext context, Auth.AuthClient authClient)
        {
            _authClient = authClient;
            _providerDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        [AllowAnonymous]
        public override Task<ProviderResponse> GetProviderById(ProviderRequest request, ServerCallContext context)
        {
            var id = request.Id;

            var eventObject = _providerDbContext.GetProvider(id);

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

        [AllowAnonymous]
        async public override Task<ProviderResponse> UpdateProvider(UpdateProviderRequest request, ServerCallContext context)
        {
            var checkActionResponse = await _authClient.CheckActionAsync(new CheckActionRequest() {
                Id= "id",
                Token = request.Token
            });

            if (checkActionResponse.Status)
            {
                var tag = _providerDbContext.Tags.FirstOrDefault(tag => tag.Value == request.Tag);
                var id = Guid.NewGuid().ToString();

                var newProvider = new ProviderDb() {
                    Title = request.Title,
                    Description = request.Description,
                    Location = request.Location,
                    Tag = tag
                };
                ProviderDb dbProviderDb = _providerDbContext.Providers.Where(providerDb => providerDb.Id == request.Id).FirstOrDefault();
                if (dbProviderDb == null)
                {
                    _providerDbContext.Add(newProvider);
                    request.Id = id;

                }
                else
                {
                    newProvider.Id = dbProviderDb.Id;
                    _providerDbContext.ChangeTracker.Clear();
                    _providerDbContext.Update(newProvider);
                }
                _providerDbContext.SaveChanges();

                var response = new ProviderResponse();
                response.Id = newProvider.Id;
                response.Title = newProvider.Title;
                response.Description = request.Description;
                response.Location = request.Location;
                response.Tag = request.Tag;
                response.Image = ByteString.Empty;

                return await Task.FromResult(response);
            }

            return await Task.FromResult(new ProviderResponse());
        }

        [AllowAnonymous]
        public override Task<ProviderPictureResponse> UpdatePicture(ProviderPictureRequest request, ServerCallContext context)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", request.FileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                var provider = _providerDbContext.Providers.Where(eventModel => eventModel.Id == request.Id).FirstOrDefault();
                provider.PictureUri = path;
                _providerDbContext.Update(provider);
                _providerDbContext.SaveChanges();
                stream.Write(request.Image.ToByteArray());
                return Task.FromResult(new ProviderPictureResponse() { Status = true });
            }
        }
    }
}
