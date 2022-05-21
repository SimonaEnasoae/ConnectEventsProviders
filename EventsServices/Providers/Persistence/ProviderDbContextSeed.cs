using Providers.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Providers.Persistence
{
    public class ProviderDbContextSeed
    {
        public async Task SeedAsync(ProviderDbContext context)
        {
            //context.Database.EnsureCreated();
            if (!context.Providers.Any())
            {
                IEnumerable<Tag> tags = CreateTags();
                var providers = CreateProviders(tags, @"C:\Projects\Extra\ConnectEventsProviders\EventsServices\Providers\Persistence\providers.csv");
                context.Tags.AddRange(tags);
                context.Providers.AddRange(providers);
            }
            await context.SaveChangesAsync();
        }

        IEnumerable<Tag> CreateTags()
        {
            var tag =
           new Tag() {
               Id = Guid.NewGuid().ToString(),
               Value = "Food",
           };
            var tag1 =
            new Tag() {
                Id = Guid.NewGuid().ToString(),
                Value = "Clothes",
            };
            var tag2 =
           new Tag() {
               Id = Guid.NewGuid().ToString(),
               Value = "Game",
           };
            List<Tag> tags = new List<Tag>()
            {
                tag,
                tag1,
                tag2
            };
            return tags;
        }

        IEnumerable<Provider> CreateProviders(IEnumerable<Tag> tags, string csvFileEvents)
        {
            if (!File.Exists(csvFileEvents))
            {
                return null;
            }

            string[] csvheaders = { "title", "location", "ImagePath", "Tag", "id" };

            return File.ReadAllLines(csvFileEvents)
                        .Skip(1) // skip header row
                        .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                        .Select(column => CreateProvider(column, csvheaders, tags))
                        .Where(x => x != null);
        }

        private Provider CreateProvider(string[] column, string[] csvheaders, IEnumerable<Tag> tags)
        {
            var tagName = column[Array.IndexOf(csvheaders, "Tag")].Trim('"').Trim();
            var tag = tags.Where(tag => tag.Value == tagName).FirstOrDefault();
            var newProvider = new Provider() {
                Id = column[Array.IndexOf(csvheaders, "id")].Trim('"').Trim(),
                Title = column[Array.IndexOf(csvheaders, "title")].Trim('"').Trim(),
                Description = "Lorem ipsum dolort sem. ",
                Location = column[Array.IndexOf(csvheaders, "location")].Trim('"').Trim(),
                PictureUri = column[Array.IndexOf(csvheaders, "ImagePath")].Trim('"').Trim(),
                Tag = tag
            };
            return newProvider;
        }
    }
}
