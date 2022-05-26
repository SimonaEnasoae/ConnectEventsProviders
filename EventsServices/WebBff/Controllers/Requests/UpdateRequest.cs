using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBff.Controllers.Requests
{
    public class UpdateRequest
    {
        public string Id { get; set; }
        public int Status { get; set; }
    }
}
