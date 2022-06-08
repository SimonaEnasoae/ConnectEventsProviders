using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Responses;
using WebBff.Controllers.Responses.Providers;

namespace WebBff.Services
{
    public interface IProviderService
    {
        Task<ProviderModel> GetProviderByIdAsync(string id);
        Task<ProviderModel> UpdateProvider(ProviderModel provider);
        Task<bool> UpdatePicture(FileModel file);

    }
}
