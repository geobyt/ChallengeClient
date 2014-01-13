using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeClient.Models
{
    public class Challenge
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public string Message { get; set; }

        public Coordinate Location { get; set; }

        public Status ProgressStatus { get; set; }
    }

    public class Coordinate
    {
        public double clat { get; set; }

        public double clong { get; set; }

        public override string ToString()
        {
            return string.Format("({0}, {1})", clat, clong);
        }
    }

    public enum Status
    {
        Locked,
        Unlocked,
        InProgress
    }
}
