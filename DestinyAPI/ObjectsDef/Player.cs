using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyAPI
{
    public class Player
    {
        public string GamerTag { get; set; }
        public MembershipType type { get; set; }
        public List<Character> Characters { get; set; }
        public int Grimoire { get; set; }
        //public string MainClan { get; set; }
        //public string MainClanTag { get; set; }
        public string MembershipId { get; set; }
        public List<ItemBase> Items { get; set; }
        public List<ItemGear> Gear
        {
            get
            {
                return Items != null ? Items
                    .OfType<ItemGear>()

                    .ToList() : null;
            }
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
        public List<ItemBase> Items { get; set; }
    }

    public class ItemBase
    {
        public ItemBase() { }

        public ItemBase(Int64 itemHash, string itemId, object dbData, int quantity, object bucketData, bool isGridComplete, int transferStatus, int state, int characterIndex, string bucketHash)
        {
            this.itemHash = itemHash.ToString();
            this.itemId = itemId;
            this.dbData = dbData;
            this.quantity = quantity;
            this.bucketData = bucketData;
            this.isGridComplete = isGridComplete;
            this.transferStatus = transferStatus;
            this.state = state;
            this.characterIndex = characterIndex;
            this.bucketHash = bucketHash;
        }

        //public ItemBase(string _itemHash, string _itemId, object _dbData, int _quantity, object _bucketData, 
        //    bool _isGridComplete, int _transferStatus, int _state, int _characterIndex, string _bucketHash)
        //{
        //    itemHash = _itemHash;
        //    itemId = _itemId;
        //    dbData = (dynamic)_dbData;
        //    quantity = _quantity;
        //    bucketData = (dynamic)_bucketData;
        //    isGridComplete = _isGridComplete;
        //    transferStatus = _transferStatus;
        //    state = _state;
        //    characterIndex = _characterIndex;
        //    bucketHash = _bucketHash;
        //}
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


    }

   
}
