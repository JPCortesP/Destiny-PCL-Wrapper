using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL.Objects
{
    public class DestinyCharacter
    {
        public string Class { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public int LightLevel { get; set; }
        public int BaseLevel { get; set; }
        public string EmblemBackgroundPath { get { return "https://bungie.net/" + _EmblemBackgroundPath; } internal set { _EmblemBackgroundPath = value; } }
        private string _EmblemBackgroundPath;
        public string EmblemPath { get { return "https://bungie.net/" + _EmblemPath; } internal set { _EmblemPath = value; } }
        private string _EmblemPath;
        public string EmblemHash { get; set; }
        public string CharacterId { get; set; }

        public int ClassIdInt
        {
            get
            {
                switch (Class)
                {
                    case "Titan":
                        return 0;
                    case "Hunter":
                        return 1;
                    case "Warlock":
                        return 2;
                    default:
                        return -1;
                }
            }
        }
        

    }
}
