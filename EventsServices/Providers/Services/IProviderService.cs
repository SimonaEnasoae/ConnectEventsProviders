using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Providers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Providers.Services
{
    public interface IProviderService 
    {
        IEnumerable<Provider> GetAll();
        Provider Update(Provider newProvider);
        Provider GetProvider(string providerId);
        Tag getTagByName(string tag);
    }
}
