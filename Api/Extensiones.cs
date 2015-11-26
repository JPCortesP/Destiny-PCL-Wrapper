
using DestinyPCL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL
{
    public partial class DestinyService : IDestnyService
    {
        private async Task<string> getString(string url, CookieContainer cookies = null)
        {
            if (cookies != null)
            {
                hc = new HttpClient(new HttpClientHandler() { CookieContainer = cookies });
                var galletas = cookies.GetCookies(new Uri("https://bungie.net"));
                foreach (Cookie item in galletas)
                {
                    if (item.Name == "bungled")
                    {
                        hc.DefaultRequestHeaders.Add("X-csrf", item.Value);
                    }
                }
                galletas = cookies.GetCookies(new Uri("https://www.bungie.net"));
                foreach (Cookie item in galletas)
                {
                    if (item.Name == "bungled")
                    {
                        hc.DefaultRequestHeaders.Add("X-csrf", item.Value);
                    }
                }
            }
            else
            {
                hc = new HttpClient();
            }
            using (hc)
            {
                hc.DefaultRequestHeaders.Add("X-API-Key", this.ApiKey);
                return await hc.GetStringAsync(url);
            }

        }
        private Player convertirAPlayer(InternalTypes.PlayerResultRootObject playerResult, Player p)
        {
               
               var origen = playerResult.Response.data;
               
               foreach (var item in origen.characters)
               {
                   Character ch = new Character();
                   ch.BaseLevel = item.characterLevel;
                   ch.CharacterId = item.characterBase.characterId;
                   ch.Class = ((dynamic)Manifest.getData(DestinyPCL.Manifest.ManifestTable.Class, item.characterBase.classHash.ToString())).className;
                   ch.EmblemBackgroundPath = item.backgroundPath;
                   ch.EmblemPath = item.emblemPath;
                   ch.Gender = ((dynamic)Manifest.getData(DestinyPCL.Manifest.ManifestTable.Gender, item.characterBase.genderHash.ToString())).genderName;
                   ch.LightLevel = item.characterBase.powerLevel;
                   ch.Race = ((dynamic)Manifest.getData(DestinyPCL.Manifest.ManifestTable.Race, item.characterBase.raceHash.ToString())).raceName;

                   p.Characters.Add(ch);
               }
               Parallel.ForEach(origen.items, (item) => 
               {
                   ItemBase b;
                   if (item.primaryStat != null)
                   {
                       b = new ItemGear(
                           (long)item.itemHash,
                           item.itemId,
                           (object)Manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryItem, item.itemHash.ToString()),
                           item.quantity,
                           (object) Manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryBucket, item.bucketHash.ToString()),
                           item.isGridComplete,
                           item.transferStatus,
                           item.state,
                           item.characterIndex,
                           item.bucketHash,
                           item.damageType,
                           (long)item.damageTypeHash,
                           item.primaryStat.maximumValue,
                           (long)item.primaryStat.statHash,
                           item.primaryStat.value,
                           (object)Manifest.getData(DestinyPCL.Manifest.ManifestTable.Stat, item.primaryStat.statHash.ToString())
                           );
                   }
                   else
                   {
                       b = new ItemBase(
                           (long)item.itemHash,
                           item.itemId,
                           (object)Manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryItem, item.itemHash.ToString()).Result,
                           item.quantity,
                           (object)Manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryBucket, item.bucketHash.ToString()),
                           item.isGridComplete,
                           item.transferStatus,
                           item.state,
                           item.characterIndex,
                           item.bucketHash
                           );
                       
                   }
                   
                       p.Items.Add(b);
                   
                   
               });
               
               
               return p;
           
        }

        
    }
}
