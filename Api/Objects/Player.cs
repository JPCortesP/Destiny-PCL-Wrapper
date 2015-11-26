
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
        public string GamerTag { get; set; }
        public MembershipType type { get; set; }
        public virtual List<Character> Characters { get; set; }
        public int Grimoire { get; set; }
        //public string MainClan { get; set; }
        //public string MainClanTag { get; set; }
        public string MembershipId { get; set; }
        public virtual List<ItemBase> Items { get; set; }
        public virtual List<ItemGear> Gear
        {
            get
            {
                return Items != null ? Items
                    .OfType<ItemGear>()
                    .OrderByDescending(b=>b.primaryStats_value)
                    .ToList() : null;
            }
        }
        
    }

    public class LazyPlayer : Player
    {
        internal LazyPlayer() { }
        internal LazyPlayer(List<InternalTypes.Item> items, DestinyManifest Manifest)
        {
            this._internalItems = items;
            this._internalManifest = Manifest;
        }
        private List<InternalTypes.Item> _internalItems { get; set; }
        private DestinyManifest _internalManifest;
        //public List<InternalTypes.Character> characters { get; set; }
        private List<ItemBase> _items;
        public override List<ItemBase> Items
        {
            get
            {
                if (_items == null)
                {
                    initItems();
                }
                return _items;
            }

            set
            {
                base.Items = value;
            }
        }

        private void initItems()
        {
            _items = new List<ItemBase>();
            Parallel.ForEach(this._internalItems, (item) =>
            {
                ItemBase b;
                if (item.primaryStat != null)
                {
                    b = new ItemGear(
                        (long)item.itemHash,
                        item.itemId,
                        (object)_internalManifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryItem, item.itemHash.ToString()),
                        item.quantity,
                        (object)_internalManifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryBucket, item.bucketHash.ToString()),
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
                        (object)_internalManifest.getData(DestinyPCL.Manifest.ManifestTable.Stat, item.primaryStat.statHash.ToString())
                        );
                }
                else
                {
                    b = new ItemBase(
                        (long)item.itemHash,
                        item.itemId,
                        (object)_internalManifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryItem, item.itemHash.ToString()).Result,
                        item.quantity,
                        (object)_internalManifest.getData(DestinyPCL.Manifest.ManifestTable.InventoryBucket, item.bucketHash.ToString()),
                        item.isGridComplete,
                        item.transferStatus,
                        item.state,
                        item.characterIndex,
                        item.bucketHash
                        );

                }

                _items.Add(b);


            });
        }
    }
    

    

    

   
}
