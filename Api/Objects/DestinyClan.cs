using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL.Objects
{
    public class DestinyClan
    {
        internal DestinyClan() { }
        internal DestinyClan(int groupdId, object clanInfo, List<InternalTypes.Result> players)
        {
            this.ClanInfo = (dynamic)clanInfo;
            this.ClanId = groupdId;
            this._playersInt = players;
            this._players = new Lazy<List<BungieUser>>(fillPlayers, true);
            

        }
        public List<BungieUser> Players { get { return this._players.Value; } }
        private Lazy<List<BungieUser>> _players { get; set; }
        private List<InternalTypes.Result> _playersInt;
        public int ClanId { get; set; }
        public int MembersCount { get { return _playersInt != null ? _playersInt.Count : 0; } }
        public String ClanName { get { return ClanInfo.name; }  }
        public string ClanTag { get { return ClanInfo.clanCallsign; } }
        public string ClanMotto
        {
            get
            {
                return System.Net.WebUtility.HtmlDecode( (string)ClanInfo.motto );
            }
        }
        public dynamic ClanInfo { get; set; }

        private List<BungieUser> fillPlayers()
        {
            var r = new List<BungieUser>();
            foreach (var item in this._playersInt)
            {
                var nuevo = new BungieUser()
                {
                    GamerTag = item.user.xboxDisplayName != null ? item.user.xboxDisplayName : item.user.psnDisplayName,
                    type = item.user.xboxDisplayName != null ? DestinyMembershipType.Xbox : DestinyMembershipType.PSN


                };
                if (string.IsNullOrWhiteSpace(nuevo?.GamerTag))
                    nuevo.GamerTag = item.user.uniqueName;
                r.Add(nuevo);
                    
            }
            return r;
        }
    }
}
