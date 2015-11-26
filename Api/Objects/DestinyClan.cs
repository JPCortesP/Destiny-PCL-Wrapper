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
        public List<DestinyPlayer> Players { get { return this._players.Value; } }
        private Lazy<List<DestinyPlayer>> _players { get; set; }

        public object ClanId { get; set; }
        public String ClanName { get; set; }
        public string ClanTag { get; set; }
    }
}
