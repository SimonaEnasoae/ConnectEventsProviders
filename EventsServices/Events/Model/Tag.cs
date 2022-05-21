using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Model
{
    public class Tag
    {
        public string Id { get; init; }
        public string Value { get; init; }
        public IEnumerable<EventTag> EventTags { get; init; }

    }
}
