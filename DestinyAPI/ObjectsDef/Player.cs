using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyAPI
{
    public class Player
    {
        public string GamerTag { get; set; }
        public MembershipType type { get; set; }
        public List<object> Characters { get; set; }
        public int Grimoire { get; set; }
        public object MainClan { get; set; }
    }
}
