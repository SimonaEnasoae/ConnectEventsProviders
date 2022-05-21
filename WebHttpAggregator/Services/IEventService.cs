using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Responses;

namespace WebBff.Services
{
    public interface IEventService
    {
        Task<EventModel> GetEventByIdAsync(string id);
    }
}
