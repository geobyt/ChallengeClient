using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeClient.Models
{
    public class MapPin
    {
        public string Id { get; set; }

        public string Order { get; set; }

        public GeoCoordinate Location { get; set; }

        public string DisplayText { get; set; }
    }
}
