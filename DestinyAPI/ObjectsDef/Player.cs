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
        public List<Character> Characters { get; set; }
        public int Grimoire { get; set; }
        //public string MainClan { get; set; }
        //public string MainClanTag { get; set; }
        public string MembershipId { get; set; }
    }
    public class Character
    {
        public string Class { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public int LightLevel { get; set; }
        public int BaseLevel { get; set; }
        public string EmblemBackgroundPath { get; set; }
        public string EmblemPath { get; set; }
        public string EmblemHash { get; set; }
        public string CharacterId { get; set; }
    }
}
