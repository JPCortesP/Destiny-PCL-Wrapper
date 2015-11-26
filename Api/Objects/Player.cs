
using DestinyPCL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL.Objects
{
    public class Player
    {
        [Obsolete("Use el constructor Lazy por Favor", true)]
        internal Player() { throw new NotImplementedException(); }
        internal Player(DestinyManifest manifest, InternalTypes.Data data)
        {
            this.Manifest = manifest;
            this.data = data;
            _characters = new Lazy<List<Character>>(() => fillChars(manifest, data), true);
            _items = new Lazy<List<ItemBase>>(() => fillItems(manifest, data), true);
        }
        private DestinyManifest Manifest;
        private InternalTypes.Data data;
        public string GamerTag { get; set; }
        public MembershipType type { get; set; }
        
        public int Grimoire { get; set; }
        //public string MainClan { get; set; }
        //public string MainClanTag { get; set; }
        public string MembershipId { get; set; }
        public List<ItemBase> Items { get { return this._items.Value; } }
        public List<Character> Characters { get { return this._characters.Value; } }
        public List<ItemGear> Gear
        {
            get
            {
                return Items != null ? Items
                    .OfType<ItemGear>()
                    .OrderByDescending(b=>b.primaryStats_value)
                    .ToList() : null;
            }
        }

        private Lazy<List<Character>> _characters { get; set; }
        private Lazy<List<ItemBase>> _items { get; set; }

        private static List<Character> fillChars(DestinyManifest manifest, InternalTypes.Data data)
        {
            var lista = new List<Character>();
            Parallel.ForEach(data.characters, (item) =>
           {
               Character ch = new Character();
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
        private static List<ItemBase> fillItems(DestinyManifest manifest, InternalTypes.Data data)
        {
            var lista = new List<ItemBase>();
            Parallel.ForEach(data.items, (item) =>
            {
                ItemBase b;
                if (item.primaryStat != null)
                {
                    b = new ItemGear(
                        (long)item.itemHash,
                        item.itemId,
                        (object)manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryItem, item.itemHash.ToString()),
                        item.quantity,
                        (object)manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryBucket, item.bucketHash.ToString()),
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
                        (object)manifest.getData(DestinyPCL.Manifest.ManifestTable.Stat, item.primaryStat.statHash.ToString())
                        );
                }
                else
                {
                    b = new ItemBase(
                        (long)item.itemHash,
                        item.itemId,
                        (object)manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryItem, item.itemHash.ToString()).Result,
                        item.quantity,
                        (object)manifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryBucket, item.bucketHash.ToString()),
                        item.isGridComplete,
                        item.transferStatus,
                        item.state,
                        item.characterIndex,
                        item.bucketHash
                        );

                }

                lista.Add(b);


            });
            return lista;
        }
        
    }

    

    

    

   
}
