using GrpcAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Responses;
using LoginResponse = WebBff.Controllers.Responses.LoginResponse;
using LoginResponseGrpc = GrpcAuth.LoginResponse;

namespace WebBff.Services
{
    public class AuthService : IAuthService
    {
        private readonly Auth.AuthClient _authClient;

        public AuthService(Auth.AuthClient authClient)
        {
            _authClient = authClient;
        }

        public async Task<LoginResponse> Login(UserAuth user)
        {
            var response = await _authClient.LoginAsync(new LoginRequest() {
                Username = user.Username,
                Password = user.Password,
            });

            return new LoginResponse() {
                Status = response.Status,
                Token = response.Token,
                UserId = response.UserId,
                Type = response.Type
            };
        }
        public async Task<LogoutResponse> Logout(UserAuth user)
        {
            var response = await _authClient.LogoutAsync(new LogoutRequest() {
                Username = user.Username,
            });

            return new LogoutResponse() {
                Status = response.Status,
            };
        }
    }
}
