using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Responses.Providers;
using WebBff.Services;

namespace WebBff.Controllers
{
    [Route("api/providers")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProvidersController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        async public Task<ProviderPaginationResponse> GetAsync([FromQuery] int pageSize = 9, int pageIndex = 0)
        {

            return await (_providerService).GetProvidersAsync(pageSize, pageIndex);
        }


        [HttpPost]
        public async Task<ProviderModel> PostAsync([FromBody] ProviderModel provider)
        {
            return await _providerService.UpdateProvider(provider);
        }

        [Route("file")]
        [HttpPost]
        async public Task<bool> PostFile([FromForm] FileModel file)
        {
            return await _providerService.UpdatePicture(file);
        }
    }
}
