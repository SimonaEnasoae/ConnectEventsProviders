using Authentication.Controllers.Responses;
using Authentication.Models;
using Authentication.Persistence;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserDbContext _userDbContext;

        public AuthService(UserDbContext context)
        {
            _userDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public LoginResponse Login(UserAuth user)
        {
            LoginResponse loginRespone = new LoginResponse();
            UserAuth userDb = _userDbContext.UserAuths.Where(userDb =>
            (
                userDb.Username == user.Username && userDb.Password == user.Password
            )).First();
            if (userDb!=null)
            {
                loginRespone.Token = "token";
                loginRespone.Type = userDb.Type.ToString();
                loginRespone.UserId = userDb.Id;
                loginRespone.Status = true;
            }
            else
            {
                loginRespone.Status = false;
            }
            return loginRespone;
        }

        public ServiceResponse Logout(UserAuth user)
        {
            throw new NotImplementedException();
        }
    }
}
