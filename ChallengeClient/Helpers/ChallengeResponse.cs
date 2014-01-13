using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeClient.Helpers
{
    [DataContract]
    public class ChallengeResponse
    {
        [DataMember(Name = "_id")]
        public MongoId Id { get; set; }

        [DataMember(Name = "from")]
        public string FromPhone { get; set; }

        [DataMember(Name = "to")]
        public string ToPhone { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        [DataMember(Name = "long")]
        public double Longitude { get; set; }
    }

    [DataContract]
    public class MongoId
    {
        [DataMember(Name = "$oid")]
        public string Oid { get; set; }
    }
}
