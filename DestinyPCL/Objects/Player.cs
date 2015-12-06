
using DestinyPCL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL.Objects
{
    /// <summary>
    /// Class that represent a Player. Can't be instantiated.
    /// Properties Characters and Items (and then Gear) are Lazy, so they will be available only after you access them 
    /// the first time, so you might want to use Async to access them (They are thread-safe for the initialization). 
    /// </summary>
    public class DestinyPlayer
    {
                
        internal DestinyPlayer(DestinyManifest manifest, InternalTypes.Data data, BungieUser user)
        {
            this.Manifest = manifest;
            this.data = data;
            this.type = user.type;
            _characters = new Lazy<List<DestinyCharacter>>(() => fillChars(manifest, data), true);
            _items = new Lazy<List<DestinyItemBase>>(() => fillItems(manifest, data), true);
            if (data.characters != null)
            {
                if(data.characters.Count > 0)
                    if(data.characters.First().characterBase != null)
                        this.Grimoire = data.characters.FirstOrDefault().characterBase.grimoireScore;
                this.CharCount = data.characters.Count;
            }
        }
        private DestinyManifest Manifest;
        private InternalTypes.Data data;
        public string GamerTag { get; set; }
        public DestinyMembershipType type { get; set; }
        public BungieUser BungieUser { get { return new BungieUser(this?.GamerTag, this.type) { membershipId = this?.MembershipId, cookies = this?.cookies }; } }
        internal CookieContainer cookies { get; set; }
        public int Grimoire { get; private set; }
        public int CharCount { get; private set; }
        //public string MainClan { get; set; }
        //public string MainClanTag { get; set; }
        public string MembershipId { get; set; }
        public List<DestinyItemBase> Items { get { return this._items.Value; } }
        public List<DestinyCharacter> Characters { get { return this._characters.Value; } }
        public List<DestinyItemGear> Gear
        {
            get
            {
                return Items != null ? Items
                    .OfType<DestinyItemGear>()
                    .OrderByDescending(b=>b.primaryStats_value)
                    .ToList() : null;
            }
        }

        private Lazy<List<DestinyCharacter>> _characters { get; set; }
        private Lazy<List<DestinyItemBase>> _items { get; set; }

        private static List<DestinyCharacter> fillChars(DestinyManifest manifest, InternalTypes.Data data)
        {
            var lista = new List<DestinyCharacter>(3);
            Parallel.ForEach(data.characters, (item) =>
           {
               DestinyCharacter ch = new DestinyCharacter();
               ch.BaseLevel = item.characterLevel;
               ch.CharacterId = item.characterBase.characterId;
               ch.Class = ((dynamic)manifest.getData(DestinyPCL.Manifest.ManifestTable.Class, item.characterBase.classHash.ToString())).className;
               ch.EmblemBackgroundPath = item.backgroundPath;
               ch.EmblemPath = item.emblemPath;
               ch.Gender = ((dynamic)manifest.getData(DestinyPCL.Manifest.ManifestTable.Gender, item.characterBase.genderHash.ToString())).genderName;
               ch.LightLevel = item.characterBase.powerLevel;
               ch.Race = ((dynamic)manifest.getData(DestinyPCL.Manifest.ManifestTable.Race, item.characterBase.raceHash.ToString())).raceName;
               
               //lock (lista)
               {
                   lista.Add(ch);
               }

           });
            return lista;
        }
        private static List<DestinyItemBase> fillItems(DestinyManifest manifest, InternalTypes.Data data)
        {
            var lista = new List<DestinyItemBase>(data.items.Count);
            Parallel.ForEach(data.items, (item) =>
            {
                DestinyItemBase b;
                if (item.primaryStat != null)
                {
                    var itemData = manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryItem, item.itemHash.ToString());
                    var bucketData = manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryBucket, item.bucketHash.ToString());
                    var stathash = manifest.getData(DestinyPCL.Manifest.ManifestTable.Stat, item.primaryStat.statHash.ToString());
                    b = new DestinyItemGear(
                        (long)item.itemHash,
                        item.itemId,
                        (object) itemData,
                        item.quantity,
                        (object)bucketData,
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
                        (object)stathash
                        );
                }
                else
                {
                    var itemData = manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryItem, item.itemHash.ToString());
                    var bucketData = manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryBucket, item.bucketHash.ToString());
                    b = new DestinyItemBase(
                        (long)item.itemHash,
                        item.itemId,
                        (object)itemData,
                        item.quantity,
                        (object)bucketData,
                        item.isGridComplete,
                        item.transferStatus,
                        item.state,
                        item.characterIndex,
                        item.bucketHash
                        );

                }
                b.BaseRawObject = item as dynamic;
                b.characterIndexName = b.characterIndex == -1 ? "Vault" 
                : manifest.getData(DestinyPCL.Manifest.ManifestTable.Class, data.characters[b.characterIndex].characterBase.classHash.ToString()).className;
                lista.Add(b);


            });
            return lista;
        }
        
    }

    

    

    

   
}
