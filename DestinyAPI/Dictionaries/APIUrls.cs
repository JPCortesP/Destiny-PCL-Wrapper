using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyAPI
{
    public static class APIUrls
    {
        public static Dictionary<string, string> URls = new Dictionary<string, string>()
        {
            {"GetPlayer" //0 = [1-2] MembershipType, 1 = Gamertag
                ,"https://www.bungie.net/platform/destiny/{0}/Stats/GetMembershipIdByDisplayName/{1}" }
        };
    }
}
