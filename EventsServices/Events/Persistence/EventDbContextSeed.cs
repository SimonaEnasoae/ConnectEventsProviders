using Events.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Events.Persistence
{
    public class EventDbContextSeed
    {
        public async Task SeedAsync(EventDbContext context)
        {
            //context.Database.EnsureCreated();
            if (!context.Events.Any())
            {
                IEnumerable<Tag> tags = CreateTags();
                //var events = CreateEvents(tags, @".\Persistence\eventsLocal.csv");
                var events = CreateEvents(tags, @"/app/Persistence/events.csv");
                context.Tags.AddRange(tags);
                context.Events.AddRange(events);
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

        IEnumerable<EventDb> CreateEvents(IEnumerable<Tag> tags, string csvFileEvents)
        {
            if (!File.Exists(csvFileEvents))
            {
                return null;
            }

            string[] csvheaders = { "title", "location", "ImagePath","Id"};

            return File.ReadAllLines(csvFileEvents)
                        .Skip(1) // skip header row
                        .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                        .Select(column => CreateEvent(column, csvheaders, tags))
                        .Where(x => x != null);
        }

        private EventDb CreateEvent(string[] column, string[] csvheaders, IEnumerable<Tag> tags)
        {
            var id = column[Array.IndexOf(csvheaders, "Id")].Trim('"').Trim();
            List<EventTag> eventTags = new List<EventTag>();
            foreach(Tag tag in tags)
            {
                eventTags.Add(new EventTag() { EventId = id, TagId = tag.Id });
            }
          
            var newEvent = new EventDb() {
                Id = id,
                Title = column[Array.IndexOf(csvheaders, "title")].Trim('"').Trim(),
                Description = "Lorem ipsum dolort sem. ",
                EventTags = eventTags,
                Location = column[Array.IndexOf(csvheaders, "location")].Trim('"').Trim(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Today,
                PictureUri = column[Array.IndexOf(csvheaders, "ImagePath")].Trim('"').Trim(),
                OrganiserId= "903f82dd-4532-4be4-8ab9-bc84a00e77ba"
            };
            return newEvent;
        }
    }
}
