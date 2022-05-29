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

        public IEnumerable<ProviderDb> GetAll()
        {
            return _providerDbContext.GetAll();
        }

        public ProviderDb GetProvider(string providerId)
        {
            return _providerDbContext.GetProvider(providerId);
        }

        public Tag getTagByName(string tag)
        {
            return _providerDbContext.Tags.Where(currentTag => currentTag.Value == tag).FirstOrDefault();
        }

        public ProviderDb Update(ProviderDb newProviderDb)
        {
            ProviderDb dbProviderDb = _providerDbContext.Providers.Where(provider => provider.Id == newProviderDb.Id).FirstOrDefault();
            if (dbProviderDb == null)
            {
                var id = Guid.NewGuid().ToString();
                newProviderDb.Id = id;
                _providerDbContext.Add(newProviderDb);
            }
            else
            {
                _providerDbContext.ChangeTracker.Clear();
                _providerDbContext.Update(newProviderDb);
            }
            _providerDbContext.SaveChanges();
            return newProviderDb;
        }
    }
}
