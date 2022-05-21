using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Providers.Controllers.Responses
{
    public class FileModel
    {
        public string ProviderId { get; set; }

        public string FileName { get; set; }
        public IFormFile FormFile {get;set;}
    }
}
