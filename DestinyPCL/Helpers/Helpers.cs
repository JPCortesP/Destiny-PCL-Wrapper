using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL
{
    /// <summary>
    /// Helpers for URLs and CookieNames. Can't promise they are updated everytime.
    /// </summary>
    public static partial class AuthHelpers
    {
        public static readonly string XboxLoginUrl = "https://www.bungie.net/en/User/SignIn/Xuid?bru=%252f";
        public static readonly string PSNLoginUrl = "https://www.bungie.net/en/User/SignIn/Psnid?bru=%252f";
        
        public static readonly string[] RequiredCookieNames = new string[] { "bungledid", "bungleme", "bungleloc", "bungled" };

        /// <summary>
        /// Helper Method to check if provided System.Net.CookieContainer has the required Cookies for
        /// Auth Calls to the API. Is up to you to check before send them, and if you don't, weird behavior could
        /// arise. 
        /// </summary>
        /// <param name="cookies">The cookies you have</param>
        /// <returns>bool indicating if the CookieContainer has what it takes to make auth calls</returns>
        public static bool CheckForRequiredCookies(CookieContainer cookies)
        {
            var secureCookies = cookies?.GetCookies(new Uri("https://www.bungie.net"));
            var nonSecureCookies = cookies?.GetCookies(new Uri("http://www.bungie.net"));
            var rootsecureCookies = cookies?.GetCookies(new Uri("https://bungie.net"));
            var rootnonSecureCookies = cookies?.GetCookies(new Uri("http://bungie.net"));

            var anyHasit = new List<bool>(RequiredCookieNames.Count());
            foreach (var item in RequiredCookieNames)
            {
                var hasit = secureCookies.ContainsNamed(item) || nonSecureCookies.ContainsNamed(item) ||
                    rootsecureCookies.ContainsNamed(item) || rootnonSecureCookies.ContainsNamed(item);
                anyHasit.Add(hasit);
            }
            return anyHasit.Where(g => g == false).Count() == 0;
            
            //var cookie_bungledid
            //var cookie_bungleme
            //var cookie_bungleloc
            //var cookie_bungled

        }

        internal static bool ContainsNamed(this CookieCollection e, string cookieName)
        {
            foreach (Cookie item in e)
            {
                if (item.Name == cookieName)
                    return true;
            }
            return false;
        }

    }
}
