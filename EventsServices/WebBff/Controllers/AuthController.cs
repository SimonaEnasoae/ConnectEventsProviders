using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Responses;
using WebBff.Services;

namespace WebBff.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [Route("login")]
        [HttpPost]
        public async Task<LoginResponse> loginAsync(UserAuth user)
        {
            var loginResponse = await _authService.Login(user);

            return loginResponse;
        }
    }
}
