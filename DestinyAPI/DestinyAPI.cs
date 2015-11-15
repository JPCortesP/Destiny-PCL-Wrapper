﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DestinyAPI.InternalTypes;

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
                var url = String.Format(APIUrls.URls["SearchPlayer"], (int)user.type, user.GamerTag);
                var SearchResultJS = await hc.GetStringAsync(
                    url
                    );
                var SearchResult = Newtonsoft.Json.JsonConvert.DeserializeObject<InternalTypes.PlayerSearchResult>(SearchResultJS);
                if (SearchResult.ErrorStatus != "Success")
                {
                    return null;
                }
                url = string.Format(APIUrls.URls["GetPlayer"], (int)user.type, SearchResult.Response);
                var PlayerResultJS = await hc.GetStringAsync(url);
                var PlayerResult = await Task.Run(() =>
                       Newtonsoft.Json.JsonConvert.DeserializeObject<InternalTypes.PlayerResultRootObject>(PlayerResultJS));
                return await ConvertirAPlayer(PlayerResult, user);



            }
        }

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
                   Grimoire = pr.grimoireScore,
                   MembershipId = pr.membershipId,
                   type = user.type,
                   Characters = new List<Character>()
               };
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

                   pl.Characters.Add(pers);

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
    }
}