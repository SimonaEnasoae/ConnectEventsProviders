using Events.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Model
{
    public class EventDb
    {
        public string Id { get; init; }

        public string Title { get; init; }
        public string Description { get; init; }
        public string Location { get; init; }
        public IEnumerable<EventTag> EventTags { get; init; }

        [Column("start_date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; init; }
        [Column("end_date")]
        [DataType(DataType.Date)]

        public DateTime EndDate { get; init; }
        [Column("organiser_id")]
        public string OrganiserId { get; init; }
        [Column("picture_uri")]
        public string PictureUri { get; set; }

    }
}
