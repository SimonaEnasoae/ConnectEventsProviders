using Authentication.Models;
using Authentication.Persistence;
using Grpc.Core;
using GrpcAuth;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Grpc
{
    public class AuthService : Auth.AuthBase
    {
        private readonly UserDbContext _userDbContext;

        public AuthService(UserDbContext context)
        {
            _userDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        [AllowAnonymous]
        public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            LoginResponse loginRespone = new LoginResponse();
            UserAuth userDb = _userDbContext.UserAuths.Where(userDb =>
            (
                userDb.Username == request.Username && userDb.Password == request.Password
            )).First();
            if (userDb != null)
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
            return Task.FromResult(loginRespone);
        }
    }
}
