using GrpcProvider;
using System;

namespace WebBff.Controllers.Responses.Providers
{
    public class ProviderModel
    {
        private ProviderResponse provider;

        public string Id { get; set; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Location { get; init; }

        public string Tag { get; init; }

        public Byte[] Image { get; set; }
        public string Token { get; set; }


        public ProviderModel() { }

        public ProviderModel(ProviderResponse baseProvider)
        {
            Id = baseProvider.Id;
            Title = baseProvider.Title;
            Location = baseProvider.Location;
            Description = baseProvider.Description;
            Tag = baseProvider.Tag;
            Image = baseProvider.Image.ToByteArray();
        }
    }
}
