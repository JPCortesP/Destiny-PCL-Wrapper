using Api.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public partial class API : IApi
    {
        private async Task<string> getString(string url, CookieContainer cookies = null)
        {
            if (cookies != null)
            {
                throw new NotImplementedException("Implemente las cookies weon!");
            }
            return await hc.GetStringAsync(url);
        }
        private async Task<Player> convertirAPlayer(InternalTypes.PlayerResultRootObject playerResult, Player p)
        {
            if (p == null)
                p = new Player();
            var origen = playerResult.Response.data;
            p.Characters = new List<Character>();
            p.Items = new List<ItemBase>();
            foreach (var item in origen.characters)
            {
                Character ch = new Character();
                ch.BaseLevel = item.characterLevel;
                ch.CharacterId = item.characterBase.characterId;
                ch.Class = Manifest.getData(Api.Manifest.ManifestTable.Class, item.characterBase.classHash.ToString());
                ch.EmblemBackgroundPath = item.backgroundPath;
                ch.EmblemPath = item.emblemPath;
                ch.Gender = Manifest.getData(Api.Manifest.ManifestTable.Gender, item.characterBase.genderHash.ToString());
                ch.LightLevel = item.characterBase.powerLevel;
                ch.Race = Manifest.getData(Api.Manifest.ManifestTable.Race, item.characterBase.raceHash.ToString());
                
                p.Characters.Add(ch);
            }
            foreach (var item in origen.items)
            {
                ItemBase b;
                if (item.primaryStat != null)
                {
                    b = new ItemGear(
                        (long)item.itemHash,
                        item.itemId,
                        (object)Manifest.getData(Api.Manifest.ManifestTable.InventoryItem, item.itemHash.ToString() ),
                        item.quantity,
                        (object)Manifest.getData(Api.Manifest.ManifestTable.InventoryBucket,item.bucketHash.ToString() ),
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
                        (object)Manifest.getData(Api.Manifest.ManifestTable.Stat,item.primaryStat.statHash.ToString())

                        );
                }
                else
                {
                    b = new ItemBase(
                        (long)item.itemHash,
                        item.itemId,
                        (object)Manifest.getData(Api.Manifest.ManifestTable.InventoryItem, item.itemHash.ToString()),
                        item.quantity,
                        (object)Manifest.getData(Api.Manifest.ManifestTable.InventoryBucket, item.bucketHash.ToString()),
                        item.isGridComplete,
                        item.transferStatus,
                        item.state,
                        item.characterIndex,
                        item.bucketHash
                        )
                    ;
                }
                p.Items.Add(b);
            }

            return p;
        }
    }
}
