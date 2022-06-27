using Authentication.Controllers.Responses;
using Authentication.Model;
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
        private readonly Auth.AuthClient _authClient;

        public AuthService(UserDbContext context)
        {
            _userDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public LoginResponse Login(UserAuth user)
        {
            LoginResponse loginRespone = new LoginResponse();
            var token = Guid.NewGuid().ToString();
            UserAuth userDb = _userDbContext.UserAuths.Where(userDb =>
            (
                userDb.Username == user.Username && userDb.Password == user.Password
            )).First();

            if (userDb!=null)
            {
                _userDbContext.Connections.Add(new Connection() { Id = Guid.NewGuid().ToString(), UserId = userDb.Id, Token = token });
                _userDbContext.SaveChanges();

                loginRespone.Token = token;
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
