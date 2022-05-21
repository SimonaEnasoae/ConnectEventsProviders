using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Controllers.Responses
{
    public class LoginResponse
    {
        public bool Status { get; set; }

        public string Token { get; set; }

        public string Type { get; set; }

        public string UserId { get; set; }

    }
}
