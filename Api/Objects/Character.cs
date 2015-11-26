using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL.Objects
{
    public class Character
    {
        public string Class { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public int LightLevel { get; set; }
        public int BaseLevel { get; set; }
        public string EmblemBackgroundPath { get { return "https://bungie.net/" + _EmblemBackgroundPath; } set { _EmblemBackgroundPath = value; } }
        private string _EmblemBackgroundPath;
        public string EmblemPath { get { return "https://bungie.net/" + _EmblemPath; } set { _EmblemPath = value; } }
        private string _EmblemPath;
        public string EmblemHash { get; set; }
        public string CharacterId { get; set; }

    }
}
