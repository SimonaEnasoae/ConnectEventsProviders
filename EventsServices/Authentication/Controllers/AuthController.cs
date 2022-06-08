using Authentication.Controllers.Responses;
using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController( IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IEnumerable<UserAuth> Get()
        {
            UserAuth[] users = new UserAuth[] {
            new UserAuth { Username = "person1@gmail.com",Password="1234" },
            new UserAuth { Username = "person1@gmail.com",Password="1234" }
            };
            return users;
        }

        [Route("login")]
        [HttpPost]
        public LoginResponse login(UserAuth user)
        {
            LoginResponse loginResponse = _authService.Login(user);
            
            return loginResponse;
        }

    }
}
