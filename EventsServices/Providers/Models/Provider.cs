using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Providers.Models
{
    public class Provider
    {
        public string Id { get; set; }

        public string Title { get; init; }
        public string Description { get; init; }
        public string Location { get; init; }
        [Column("picture_uri")]
        public string PictureUri { get; set; }

        public Tag Tag { get; set; }
    }
}
