using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CSharp;


namespace DestinyAPI.Test
{
    [TestClass]
    public class DestinyApiTests
    {
        [TestMethod]
        public void ReturnsPlayerOrNull()
        {
            DestinyAPI api = new DestinyAPI();
            BungieUser user = new BungieUser() { GamerTag = "JPCortesP", type = MembershipType.PSN };//Not found
            BungieUser user1 = new BungieUser() { GamerTag = "JPCortesP", type = MembershipType.Xbox };//Correct

            var player = api.GetPlayer(user1).Result;
            Assert.IsInstanceOfType(player, typeof(Player));

            //Not Found
                var OtroPlayer = api.GetPlayer(user).Result;
            Assert.IsNull(OtroPlayer);
        }
        [TestMethod]
        public void ItemsAreNotNull()
        {
            DestinyAPI api = new DestinyAPI();
            BungieUser user1 = new BungieUser() { GamerTag = "JPCortesP", type = MembershipType.Xbox };//Correct

            var player = api.GetPlayer(user1).Result;

            Assert.IsNotNull(player.Items);
        }

        [TestMethod]
        public void ManifestReturnsAManifest()
        {
            var manifest = db.Manifest.Create(false);

            Assert.IsInstanceOfType(manifest, typeof(db.Manifest));
        }

        [TestMethod]
        public void ShouldReturnGjallahorn()
        {
            DestinyAPI api = new DestinyAPI();
            BungieUser user1 = new BungieUser() { GamerTag = "JPCortesP", type = MembershipType.Xbox };//Correct
            var player = api.GetPlayer(user1).Result;
            var Manifest = db.Manifest.Create();
            var items = (from ex in Manifest.Tables
                        where ex.TableName == "DestinyInventoryItemDefinition"
                        select ex).First();
            Assert.IsNotNull(items);
            var gjallahorn = (from ex in items.Rows
                             where ex.Json.Contains("Gjallarhorn")
                             select ex).First();
            var enCaracter = player.Items.Where(g => g.itemHash.ToString() == "1274330687").First();
            var flavorText = (string)gjallahorn.Data.itemDescription;

            Assert.IsTrue(flavorText.Contains("beauty in destruction"));
            //var gjalahorn = from ex in Manifest.Tables
        }

        [TestMethod]
        public void JPDoesntHaveThe_FUCKING_Messenger()
        {
            DestinyAPI api = new DestinyAPI();
            var player = api.GetPlayer(new BungieUser() {GamerTag = "JPCortesP", type = MembershipType.Xbox }).Result;
            var Manifest = db.Manifest.Create();
            var items = (from ex in Manifest.Tables
                         where ex.TableName == "DestinyInventoryItemDefinition"
                         select ex).First();
            var messenger = (from ex in items.Rows
                              where ex.Json.Contains("The Messenger")
                              select ex).First();
            var hasit = player.Items.Where(g => g.itemHash == messenger.id.ToString()).Count();
            Assert.IsTrue(hasit == 0);
        }

       
    }
    
}
