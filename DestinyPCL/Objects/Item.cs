using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL.Objects
{
    public class DestinyItemBase
    {
        public DestinyItemBase() { }

        public DestinyItemBase(Int64 itemHash, string itemId, object dbData, int quantity, object bucketData, bool isGridComplete, int transferStatus, int state, int characterIndex, string bucketHash)
        {
            this.itemHash = itemHash.ToString();
            this.itemId = itemId;
            this.dbData = (dynamic)dbData;
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
        /// <summary>
        /// -1 for Vault, 0,1,2 for any character
        /// </summary>
        public int characterIndex { get; set; }
        public string characterIndexName { get; set; }
        public string bucketHash { get; set; }
        public string itemName { get { return dbData!=null?(string)dbData?.itemName : "Unknown"; } }
        public string itemDescription { get { return (string)dbData.itemDescription; } }
        public string itemTypeName { get { return dbData!=null? dbData?.itemTypeName: "Unknown"; } }
        public string bucketName { get { return this.bucketData?.bucketName?.Value; } }
        /// <summary>
        /// Base Raw Object, as the name says it. 
        /// </summary>
        public dynamic BaseRawObject { get; set; }
        public override string ToString()
        {
            return itemName;
        }

    }

    public class DestinyItemGear : DestinyItemBase
    {
        //private int maximumValue;
        //private string statHash;
        //private int value;

        public DestinyItemGear() { }
        public DestinyItemGear(DestinyItemBase item) { }

        public DestinyItemGear(Int64 itemHash, string itemId, object dbData, int quantity, object bucketData, bool isGridComplete,
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
        public List<ItemStatBase> BaseStats { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="charClass">0 Titan, 2 Warlock, 1 Hunter. </param>
        /// <returns></returns>
        public bool canBeEquippedOn(int charClass)
        {
            if (charClass != 1 && charClass != 2 && charClass != 0)
            {
                return false;
            }
            switch (this.itemTypeName)
            {
                case "Sniper Rifle":case "Fusion Rifle":case "Sword":case "Auto Rifle":case "Pulse Rifle":case "Vehicle":case "Sidearm":
                case "Rocket Launcher":case "Ghost Shell":
                case "Hand Cannon":
                case "Scout Rifle":
                case "Shotgun":
                case "Machine Gun":
                    return true;


                case "Hunter Artifact": case "Hunter Cloak":
                    return charClass == 1;
                case "Warlock Bond": case "Warlock Artifact":
                    return charClass == 2;
                case "Titan Artifact":case "Titan Mark":
                    return charClass == 0;

                
                case "Helmet":
                case "Leg Armor":
                case "Gauntlets":
                case "Chest Armor":
                    return charClass == (int)dbData.classType?.Value;


                case "Unknown":
                default:
                    return false;
            }

        }


    }
}
