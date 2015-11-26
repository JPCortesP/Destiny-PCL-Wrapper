using DestinyPCL.Objects;
using System.Net;

namespace DestinyPCL.Objects
{
    public class BungieUser
    {
        public string GamerTag { get; set; }
        public DestinyMembershipType type { get; set; }

        public CookieContainer cookies { get; set; }
    }
}
