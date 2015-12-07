using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL.Objects
{
    public class ItemStatBase
    {
        public string statName { get; set; }
        public string statHash { get; set; }
        public int value { get; set; }
        public int minimum { get; set; }
        public int maximum { get; set; }
    }
}
