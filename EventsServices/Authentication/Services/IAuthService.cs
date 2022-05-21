using Authentication.Controllers.Responses;
using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface IAuthService
    {
        LoginResponse Login(UserAuth user);
        ServiceResponse Logout(UserAuth user);

    }
}
