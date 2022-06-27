using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using GrpcEvent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebBff.Controllers.Responses.Events;

namespace WebBff.Services
{
    public class EventService : IEventService
    {
        private readonly Event.EventClient _eventClient;

        public EventService(Event.EventClient eventClient)
        {
            _eventClient = eventClient;
        }
        public async Task<EventModel> GetEventByIdAsync(string id)
        {
            var eventObj = await _eventClient.GetEventByIdAsync(new EventRequest() { Id = id});
            return new EventModel(eventObj);

        }

        public async Task<EventsPaginationResponse> GetEventsAsync(string organiserId, int pageSize, int pageIndex)
        {

            var eventsPagination = await _eventClient.GetEventsAsync(new EventsRequest() {
                OrganiserId = organiserId ?? "",
                PageSize = pageSize,
                PageIndex = pageIndex
            });

            var events = new List<EventModel>();

            foreach (var eventObj in eventsPagination.Data)
            {
                events.Add(new EventModel(eventObj));
            }

            var eventPagination = new EventsPaginationResponse() {
                TotalEvents = eventsPagination.TotalEvents,
                TotalPages = eventsPagination.TotalPages,
                Data = events
            };

            return eventPagination;
        }

        async public Task<EventModel> SaveEvent(EventModel eventModel)
        {
            var request = new UpdateEventRequest() {
                Title = eventModel.Title,
                Description = eventModel.Description,
                Location = eventModel.Location,
                OrganiserId = eventModel.OrganiserId,
                EndDate = Timestamp.FromDateTime(eventModel.EndDate),
                StartDate = Timestamp.FromDateTime(eventModel.StartDate),
                Token = eventModel.Token
            };
            foreach (var tag in eventModel.Tags)
            {
                request.Tags.Add(new Tag() { Id = "", Value = tag });
            }
            var eventObj = await _eventClient.SaveEventAsync(request);


            return new EventModel(eventObj);
        }

        async public Task<bool> SavePicture(FileModel file)
        {
            using (var ms = new MemoryStream())
            {
                file.FormFile.CopyTo(ms);

                var response = await _eventClient.SavePictureAsync(new EventPictureRequest() {
                    Id = file.EventId,
                    FileName = file.FileName,
                    Image = ByteString.CopyFrom(ms.ToArray())
                });
                return response.Status;
            }
        }
    }
}
