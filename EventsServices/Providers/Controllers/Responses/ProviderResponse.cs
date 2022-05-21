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

        public ProviderResponse(Provider baseProvider)
        {
            Id = baseProvider.Id;
            Title = baseProvider.Title;
            Location = baseProvider.Location;
            Description = baseProvider.Description;
            Tag = baseProvider.Tag.Value;
        }
        public ProviderResponse() { }


    }
}
