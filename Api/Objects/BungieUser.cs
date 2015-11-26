﻿using DestinyPCL.Objects;
using System.Net;

namespace DestinyPCL.Objects
{
    public class BungieUser
    {
        public string GamerTag { get; set; }
        public MembershipType type { get; set; }

        public CookieContainer cookies { get; set; }
    }
}
