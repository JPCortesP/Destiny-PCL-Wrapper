using DestinyPCL.Objects;
using System.Net;

namespace DestinyPCL.Objects
{
    public class BungieUser
    {
        public string GamerTag { get; set; }
        public DestinyMembershipType type { get; set; }

        public CookieContainer cookies { get; set; }
        internal string membershipId { get; set; }

        public BungieUser() { }
        public BungieUser(string _gamertag, DestinyMembershipType _type)
        {
            this.GamerTag = _gamertag;
            this.type = _type;
        }
        public BungieUser(string _gamertag, DestinyMembershipType _type, CookieContainer _cookies)
        {
            this.GamerTag = _gamertag;
            this.type = _type;
            this.cookies = _cookies;
        }

    }
}
