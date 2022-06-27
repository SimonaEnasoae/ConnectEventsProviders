using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Model
{
    public class Connection
    {
        public string Id { get; set; }

        public string UserId { get; init; }
        public string Token { get; init; }
    }
}
