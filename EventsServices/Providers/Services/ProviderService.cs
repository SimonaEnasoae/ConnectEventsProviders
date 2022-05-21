using Providers.Models;
using Providers.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Providers.Services
{
    public class ProviderService :IProviderService
    {
        ProviderDbContext _providerDbContext;
        public ProviderService(ProviderDbContext context)
        {
            _providerDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Provider> GetAll()
        {
            return _providerDbContext.GetAll();
        }

        public Provider GetProvider(string providerId)
        {
            return _providerDbContext.GetProvider(providerId);
        }

        public Tag getTagByName(string tag)
        {
            return _providerDbContext.Tags.Where(currentTag => currentTag.Value == tag).FirstOrDefault();
        }

        public Provider Update(Provider newProvider)
        {
            Provider dbProvider = _providerDbContext.Providers.Where(provider => provider.Id == newProvider.Id).FirstOrDefault();
            if (dbProvider == null)
            {
                var id = Guid.NewGuid().ToString();
                newProvider.Id = id;
                _providerDbContext.Add(newProvider);
            }
            else
            {
                _providerDbContext.ChangeTracker.Clear();
                _providerDbContext.Update(newProvider);
            }
            _providerDbContext.SaveChanges();
            return newProvider;
        }
    }
}
