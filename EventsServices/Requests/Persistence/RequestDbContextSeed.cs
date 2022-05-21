using Requests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Persistence
{
    public class RequestDbContextSeed
    {
        public async Task SeedAsync(RequestsDbContext context)
        {
            if (!context.RequestEvents.Any())
            {
                RequestEvent requestEvent = new RequestEvent() {
                    Id = Guid.NewGuid().ToString(),
                    EventId = "01014362-c1a2-4211-bfff-bf6aff12cb22",
                    ReceiverId = "903f82dd-4532-4be4-8ab9-bc84a00e77ba",
                    SenderId = "8d9806ae-4f72-408f-b386-7a4e525a7417",
                    Status = Status.Pending
                };

                RequestEvent requestEvent1 = new RequestEvent() {
                    Id = Guid.NewGuid().ToString(),
                    EventId = "0343d90f-e8a4-41be-8be3-94992f253880",
                    ReceiverId = "903f82dd-4532-4be4-8ab9-bc84a00e77ba",
                    SenderId = "8d9806ae-4f72-408f-b386-7a4e525a7417",
                    Status = Status.Pending
                };
                RequestEvent requestEvent2 = new RequestEvent() {
                    Id = Guid.NewGuid().ToString(),
                    EventId = "14d8fd31-d986-4ce4-9be7-b2dcf6ec79f6",
                    ReceiverId = "903f82dd-4532-4be4-8ab9-bc84a00e77ba",
                    SenderId = "8d9806ae-4f72-408f-b386-7a4e525a7417",
                    Status = Status.Pending
                };
                context.RequestEvents.Add(requestEvent);
                context.RequestEvents.Add(requestEvent1);
                context.RequestEvents.Add(requestEvent2);

                await context.SaveChangesAsync();
            }
        }
    }
}
