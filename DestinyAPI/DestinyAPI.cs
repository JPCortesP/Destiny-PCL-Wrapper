using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DestinyAPI.InternalTypes;
using System.Net;

namespace DestinyAPI
{
    public sealed class DestinyAPI
    {
        private string _APIKEY;
        
        public db.Manifest DestinyData {get; set;}
        /// <summary>
        /// Creates a new instance of DestinyAPI. Use the created object to performs actions. 
        /// Can throw exceptions if you don't specify any parameter, or if you set initManifest to true (for example,
        /// file operations. 
        /// </summary>
        /// <param name="APIKEY">API Key para Bungie.NET</param>
        /// <param name="initManifest">True para inicializar Manifesto. True por defecto.</param>
        public DestinyAPI(string APIKEY = "6def2424db3a4a8db1cef0a2c3a7807e", bool initManifest = true)
        {
            this._APIKEY = APIKEY;
            if (initManifest)
            {
                DestinyData = db.Manifest.Create();
            }
            
        }
        /// <summary>
        /// Returns a Player Object, with a [optional] Character Collection, from
        /// a user object. You can build your own user object. IF you provided the constructor 
        /// with a Cookie collection, then you can send an empty BungieUser object
        /// </summary>
        /// <param name="user">The user to search for</param>
        /// <returns>Player object if found, null otherwise</returns>
        public async Task<Player> GetPlayer(BungieUser user)
        {
                var url = String.Format(APIUrls.URls["SearchPlayer"], (int)user.type, user.GamerTag);
                var SearchResultJS = await GetStringAsync(url, user.cookies);
                var SearchResult = Newtonsoft.Json.JsonConvert.DeserializeObject<InternalTypes.PlayerSearchResult>(SearchResultJS);
                if (SearchResult.ErrorStatus != "Success")
                {
                    return null;
                }
                url = string.Format(APIUrls.URls["GetPlayerDetail"], (int)user.type, SearchResult.Response);
                var PlayerResultJS = await GetStringAsync(url, user.cookies);
                var PlayerResult = await Task.Run(() =>
                       Newtonsoft.Json.JsonConvert.DeserializeObject<InternalTypes.PlayerResultRootObject>(PlayerResultJS));
                var retorno = await ConvertirAPlayer(PlayerResult, user);
                return retorno;

        }

        private async Task<string> GetStringAsync(string url, CookieContainer cookies = null)
        {
            HttpClient hc;
            if (cookies !=null)
            {
                hc = new HttpClient(new HttpClientHandler() { CookieContainer = cookies });
            }
            else
                hc = new HttpClient();
            using (hc)
            {
                hc.DefaultRequestHeaders.Add("X-API-Key", _APIKEY);

                return await hc.GetStringAsync(url);

            }
        }


        #region Helpers
        private async Task<Player> ConvertirAPlayer(PlayerResultRootObject playerResult, BungieUser user)
        {
            if (playerResult.ErrorStatus != "Success")
            {
                return null;
            }
            var pr = playerResult.Response.data;
            return await Task.Run(() =>
           {
               var pl = new Player()
               {
                   GamerTag = user.GamerTag,
                   Grimoire = pr.characters.First().characterBase.grimoireScore,
                   MembershipId = pr.characters.First().characterBase.membershipId,
                   
                   type = user.type,
                   Characters = new List<Character>(),
                   Items = new List<ItemBase>()
               };
               foreach (var item in pr.items)
               {
                   var newi = new ItemBase();

                   newi.bucketHash = item.bucketHash;
                   newi.characterIndex = item.characterIndex;
                   newi.damageType = item.damageType;
                   newi.damageTypeHash = item.damageTypeHash;
                   newi.isGridComplete = item.isGridComplete;
                   newi.itemHash = item.itemHash.ToString();
                   newi.itemId = item.itemId;
                   newi.quantity = item.quantity;
                   newi.state = item.state;
                   newi.transferStatus = item.transferStatus;
                   if (item.primaryStat != null)
                   {
                       newi.primaryStats_maximumValue = item.primaryStat.maximumValue;
                       newi.primaryStats_statHash = item.primaryStat.statHash;
                       newi.primaryStats_value = item.primaryStat.value;
                   }
                   if (this.DestinyData != null)
                   {
                       newi.dbData = this.DestinyData.GetItemData(newi.itemHash);
                   }

                   pl.Items.Add(newi);
               }
               int charCount = 0;
               foreach (var item in pr.characters)
               {
                   var pers = new Character();
                   pers.BaseLevel = item.characterLevel;
                   pers.EmblemBackgroundPath = item.backgroundPath;
                   pers.EmblemHash = item.emblemHash.ToString();
                   pers.EmblemPath = item.emblemPath;
                   pers.LightLevel = item.characterBase.powerLevel;
                   pers.CharacterId = item.characterBase.characterId;

                   pers.Class = getClass(item.characterBase.classType);
                   pers.Gender = getGender(item.characterBase.genderType);
                   pers.Race = getRace(item.characterBase.raceHash);
                   pers.Items = pl.Items.Where(g => g.characterIndex == charCount).ToList();
                   pl.Characters.Add(pers);
                   charCount++;
               }
               

               return pl;
           }
            );
        }
        private string getRace(object v)
        {
            switch (v.ToString())
            {
                case "3887404748":
                    return "Human";
                case "2803282938":
                    return "Awoken";
                case "898834093":
                    return "Exo";
                default:
                    return v.ToString();
            }
        }

        private string getGender(long genderType)
        {
            return genderType == 0 ? "Male" : "Female";
        }

        private string getClass(long classType)
        {
            switch (classType)
            {
                case 0:
                    return "Titan";
                case 1:
                    return "Hunter";
                case 2:
                    return "Warlock";
                default:
                    return classType.ToString();
            }
        }
        #endregion

    }
}
