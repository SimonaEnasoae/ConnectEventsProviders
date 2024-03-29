﻿using GrpcAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBff.Controllers.Responses;
using WebBff.Services;
using LoginResponse = WebBff.Controllers.Responses.LoginResponse;

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

        [Route("logout")]
        [HttpPost]
        public async Task<LogoutResponse> logoutAsync(UserAuth user)
        {
            var logoutResponse = await _authService.Logout(user);

            return logoutResponse;
        }
    }
}
