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
        public LazyPlayer() { }
        public LazyPlayer(List<InternalTypes.Item> items, DestinyManifest Manifest)
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
    public class Character
    {
        public string Class { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public int LightLevel { get; set; }
        public int BaseLevel { get; set; }
        public string EmblemBackgroundPath { get { return "https://bungie.net/" + _EmblemBackgroundPath; } set { _EmblemBackgroundPath = value; } }
        private string _EmblemBackgroundPath;
        public string EmblemPath { get { return "https://bungie.net/" + _EmblemPath; } set { _EmblemPath = value; } }
        private string _EmblemPath;
        public string EmblemHash { get; set; }
        public string CharacterId { get; set; }
        
    }

    public class ItemBase
    {
        public ItemBase() { }

        public ItemBase(Int64 itemHash, string itemId, object dbData, int quantity, object bucketData, bool isGridComplete, int transferStatus, int state, int characterIndex, string bucketHash)
        {
            this.itemHash = itemHash.ToString();
            this.itemId = itemId;
            this.dbData =(dynamic)dbData;
            this.quantity = quantity;
            this.bucketData = (dynamic)bucketData;
            this.isGridComplete = isGridComplete;
            this.transferStatus = transferStatus;
            this.state = state;
            this.characterIndex = characterIndex;
            this.bucketHash = bucketHash;
        }

        
        public string itemHash { get; set; }
        public string itemId { get; set; }
        public string Icon { get { return "https://bungie.net/" + (string)dbData.icon; } }
        public dynamic dbData { get; set; }
        public int quantity { get; set; }
        public dynamic bucketData { get; set; }
        public bool isGridComplete { get; set; }
        public int transferStatus { get; set; }
        public int state { get; set; }
        public int characterIndex { get; set; }
        public string bucketHash { get; set; }
        public string itemName { get { return (string)dbData.itemName; } }
        public string itemDescription { get { return (string)dbData.itemDescription; } }
        public string itemTypeName { get { return dbData.itemTypeName; } }
        public override string ToString()
        {
            return itemName;
        }

    }
    
    public class ItemGear : ItemBase
    {
        //private int maximumValue;
        //private string statHash;
        //private int value;

        public ItemGear() { }
        public ItemGear(ItemBase item) { }

        public ItemGear(Int64 itemHash, string itemId, object dbData, int quantity, object bucketData, bool isGridComplete, 
            int transferStatus, int state, int characterIndex, string bucketHash, int damageType,
            Int64 damageTypeHash, int maximumValue, Int64 statHash, int value, object statData) 
            : base(itemHash, itemId, dbData, quantity, bucketData, isGridComplete, transferStatus, state, characterIndex, bucketHash)
        {
            this.damageType = damageType;
            this.damageTypeHash = damageTypeHash.ToString();
            this.primaryStats_maximumValue = maximumValue;
            this.primaryStats_statHash = statHash.ToString();
            this.primaryStats_value = value;
            this.statData = (dynamic)statData;
        }

        public string damageTypeHash { get; set; }
        public string primaryStats_statHash { get; set; }
        public int primaryStats_value { get; set; }
        public int primaryStats_maximumValue { get; set; }
        public int damageType { get; set; }
        public dynamic statData { get; set; }

        public bool Stats_Present { get { return primaryStats_statHash != null; } }
        public string primaryStats_Name { get { return primaryStats_statHash != null ? statData.statName : null; } }
        public string tierTypeName { get { return dbData.tierTypeName; } }
        public List<StatBase> BaseStats { get; set; }


    }

    public class StatBase
    {
        public string statName { get; set; }
        public string statHash { get; set; }
        public int value { get; set; }
        public int minimum { get; set; }
        public int maximum { get; set; }
    }

   
}
