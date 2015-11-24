using Api.Objects;
using System.Net;

namespace Api.Objects
{
    public class BungieUser
    {
        public string GamerTag { get; set; }
        public MembershipType type { get; set; }

        public CookieContainer cookies { get; set; }
    }
}
