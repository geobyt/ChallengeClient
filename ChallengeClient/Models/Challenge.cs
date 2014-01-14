using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeClient.Models
{
    public class Challenge
    {
        public string Id { get; set; }

        public string Order { get; set; }

        public string Address { get; set; }

        public string Message { get; set; }

        public GeoCoordinate Location { get; set; }

        public Status ProgressStatus { get; set; }
    }

    public enum Status
    {
        Locked,
        Unlocked,
        InProgress
    }
}
