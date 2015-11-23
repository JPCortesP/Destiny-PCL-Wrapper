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
            {"SearchPlayer" //0 = [1-2] MembershipType, 1 = Gamertag
                ,"http://www.bungie.net/platform/destiny/{0}/Stats/GetMembershipIdByDisplayName/{1}" }
            
            , { "GetPlayerDetail", //0 =[1-2] MembershipType, 1 = MembershipID
                "http://www.bungie.net/platform/destiny/{0}/Account/{1}/Items/" }
        };
    }
}
