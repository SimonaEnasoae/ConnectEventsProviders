using GrpcEvent;
using System.Threading.Tasks;
using WebBff.Controllers.Responses.Events;

namespace WebBff.Services
{
    public interface IEventService
    {
        Task<EventModel> GetEventByIdAsync(string id);

        Task<EventsPaginationResponse> GetEventsAsync(string organiserId, int pageSize, int pageIndex);
        Task<EventModel> SaveEvent(EventModel eventModel);
        Task<bool> SavePicture(FileModel file);
    }
}
