using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL
{
    /// <summary>
    /// Helpers for URLs and CookieNames. Can't promise they are updated everytime.
    /// </summary>
    public static class Helpers
    {
        public static readonly string XboxLoginUrl = "https://www.bungie.net/en/User/SignIn/Xuid?bru=%252f";
        public static readonly string PSNLoginUrl = "https://www.bungie.net/en/User/SignIn/Psnid?bru=%252f";
        
        public static readonly string[] RequiredCookieNames = new string[] { "bungledid", "bungleme", "bungleloc", "bungled" };

    }
}
