using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DestinyAPI
{
    public sealed class DestinyAPI
    {
        private string _APIKEY;
        public DestinyAPI(string APIKEY = "6def2424db3a4a8db1cef0a2c3a7807e")
        {
            this._APIKEY = APIKEY;
        }
        /// <summary>
        /// Returns a Player Object, with a [optional] Character Collection, from
        /// a user object. You can build your own user object. 
        /// </summary>
        /// <param name="user">The user to search for</param>
        /// <returns>Player object if found, null otherwise</returns>
        public async Task<Player> GetPlayer(BungieUser user)
        {
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.Add("X-API-Key", _APIKEY);
                var url = String.Format(APIUrls.URls["GetPlayer"], (int)user.type, user.GamerTag);
                var SearchResultJS = await hc.GetStringAsync(
                    url
                    );
                var SearchResult = Newtonsoft.Json.JsonConvert.DeserializeObject<InternalTypes.PlayerSearchResult>(SearchResultJS);
                if (SearchResult.ErrorStatus != "Success")
                {
                    return null;
                }


                var pl = new Player();
                return pl;
                
            }
        }
        
    }
}
