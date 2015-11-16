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
        public List<ItemBase> Gear
        {
            get
            {
                return Items != null ? Items
                    .Where(g => g.itemTypeName != "Titan Subclass" )
                    .Where(g => g.itemTypeName != "Warlock Subclass")
                    .Where(g => g.itemTypeName != "Hunter Subclass")
                    .Where(g => g.itemTypeName != "Vehicle")
                    .Where(g => g.itemTypeName != "Ship")
                    .Where(g => g.itemTypeName != "Armor Shader")
                    .Where(g => g.itemTypeName != "Emblem")
                    .Where(g => g.itemTypeName != "Emote")

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
        public string EmblemBackgroundPath { get; set; }
        public string EmblemPath { get; set; }
        public string EmblemHash { get; set; }
        public string CharacterId { get; set; }
        public List<ItemBase> Items { get; set; }
    }

    public class ItemBase
    {
        public string itemHash { get; set; }
        public string itemId { get; set; }
        public int quantity { get; set; }
        public int damageType { get; set; }
        public object damageTypeHash { get; set; }
        public bool isGridComplete { get; set; }
        public int transferStatus { get; set; }
        public int state { get; set; }
        public int characterIndex { get; set; }
        public object bucketHash { get; set; }
        public object primaryStats_statHash { get; set; }
        public int primaryStats_value { get; set; }
        public int primaryStats_maximumValue { get; set; }

        public dynamic dbData { get; set; }

        public string itemName { get { return (string)dbData.itemName; } }
        public string itemDescription { get { return (string)dbData.itemDescription; } }
        public string itemTypeName { get { return dbData.itemTypeName;  } }
        public string Icon { get { return "https://bungie.net/" + (string)dbData.icon;  } }

    }
   
}
