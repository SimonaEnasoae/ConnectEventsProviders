using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBff.Controllers.Responses.Events
{
    public class FileModel
    {
        public string EventId { get; set; }

        public string FileName { get; set; }
        public IFormFile FormFile {get;set;}
    }
}
