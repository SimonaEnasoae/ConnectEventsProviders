using Providers.Models;
using System;
using System.Linq;

namespace Providers.Controllers.Responses
{
    public class ProviderResponse
    {
        public string Id { get; set; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Location { get; init; }
       
        public string Tag { get; init; }

        public Byte[] Image { get; set; }

        public ProviderResponse(ProviderDb baseProviderDb)
        {
            Id = baseProviderDb.Id;
            Title = baseProviderDb.Title;
            Location = baseProviderDb.Location;
            Description = baseProviderDb.Description;
            Tag = baseProviderDb.Tag.Value;
        }
        public ProviderResponse() { }


    }
}
