using Microsoft.AspNetCore.Mvc;
using Providers.Controllers.Responses;
using Providers.Models;
using Providers.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Providers.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private IProviderService _providerService;
        public ProvidersController(IProviderService context)
        {
            _providerService = context ?? throw new ArgumentNullException(nameof(context));
        }

        [Route("providers")]
        [HttpGet]
        public ProviderPaginationResponse GetProviders([FromQuery] int pageSize = 9, [FromQuery] int pageIndex = 0)
        {
            var providers = _providerService.GetAll();

            var providersOnpage = providers
           .Skip(pageSize * pageIndex)
           .Take(pageSize);

            var providersResponse = new List<ProviderResponse>();
            foreach (Provider currentProvider in providersOnpage)
            {
                ProviderResponse providerResponse = new ProviderResponse(currentProvider);
                providerResponse.Image = System.IO.File.ReadAllBytes(currentProvider.PictureUri);
                providersResponse.Add(providerResponse);
            }
            ProviderPaginationResponse providerPagination = new ProviderPaginationResponse() {
                TotalProviders = providers.ToArray().Length,
                TotalPages = providersResponse.Count/ pageSize,
                Data = providersResponse
            };
            return providerPagination;
        }

        [Route("provider")]
        [HttpGet]
        public ProviderResponse GetProvider([FromQuery] string providerId = "")
        {
            ProviderResponse providerResponse;
            if (providerId != null && providerId != "")
            {
                var provider = _providerService.GetProvider(providerId);
                if (provider != null)
                {
                    providerResponse = new ProviderResponse(provider);
                    providerResponse.Image = System.IO.File.ReadAllBytes(provider.PictureUri);
                }
                else
                {
                    providerResponse = new ProviderResponse();

                }
            }
            else
            {
                providerResponse = new ProviderResponse();
            }
            return providerResponse;
        }

        [Route("providers")]
        [HttpPost]
        public ProviderResponse Post([FromBody] ProviderResponse provider)
        {
            Tag dbTag = _providerService.getTagByName(provider.Tag);

            var newProvider = new Provider() {
                Title = provider.Title,
                Description = provider.Description,
                Tag = dbTag,
                Location = provider.Location,
                Id=provider.Id
                //OrganiserId = "MNS",
            };

            ProviderResponse eventResponse = new ProviderResponse(newProvider);
            Provider providerDb = _providerService.Update(newProvider);
            eventResponse.Id = providerDb.Id;
            return eventResponse;
        }

        [Route("providers/file")]
        [HttpPost]
        public bool PostFile([FromForm] FileModel file)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                Provider dbProvider = _providerService.GetProvider(file.ProviderId);
                dbProvider.PictureUri = path;
                _providerService.Update(dbProvider);
                file.FormFile.CopyTo(stream);
            }
            return true;
        }
    }
}
