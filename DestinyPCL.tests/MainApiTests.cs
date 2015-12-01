using DestinyPCL.Manifest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.tests
{
    [TestClass]
    public class MainApiTests
    {
        private static string key = "6def2424db3a4a8db1cef0a2c3a7807e";
        private DestinyPCL.IDestnyService api = new DestinyPCL.DestinyService(new OnlineManifest(), key);
        [TestMethod]
        public void MainApi_ManifestNameWorks()
        {
           
            var name = api.ManifestTypeName;
            Assert.IsNotNull(name);
            Assert.IsTrue(name == "DestinyPCL.Manifest.OnlineManifest");

        }
        [TestMethod]
        public void MainApi_ReturnsPlayerCorrectly()
        {
            var player = api.getPlayerAsync(new DestinyPCL.Objects.BungieUser() { GamerTag = "JPCortesP", type = DestinyPCL.Objects.DestinyMembershipType.Xbox }).Result;
            Assert.IsNotNull(player);

            Assert.IsTrue(player.GamerTag == "JPCortesP");
            Assert.IsTrue(player.Characters.Count == 3);
            Assert.IsNotNull(player.Gear);
            Assert.IsNotNull(player.Gear.First().primaryStats_Name);
            Assert.IsNotNull(player.Characters.First().Class);
            Assert.IsNotNull(player.Characters.First().Gender);
            Assert.IsNotNull(player.Characters.First().Race);
        }
            
        [TestMethod]
        public void MainApi_GT_Is_Case_Insensitive()
        {
            var player1 = api.getPlayerAsync(new DestinyPCL.Objects.BungieUser() { GamerTag = "jpcortesp", type = DestinyPCL.Objects.DestinyMembershipType.Xbox });
            var player2 = api.getPlayerAsync(new DestinyPCL.Objects.BungieUser() { GamerTag = "JPCortesP", type = DestinyPCL.Objects.DestinyMembershipType.Xbox });

            Assert.AreEqual(player1.Result.MembershipId, player2.Result.MembershipId);
        }
        [TestMethod]
        public void MainApi_GetSeveralPlayersInLoop()
        {
            List<DestinyPCL.Objects.BungieUser> users = new List<DestinyPCL.Objects.BungieUser>()
            {
                new DestinyPCL.Objects.BungieUser() {GamerTag = "jpcortesp", type= DestinyPCL.Objects.DestinyMembershipType.Xbox },
                new DestinyPCL.Objects.BungieUser() {GamerTag="wilwong", type= DestinyPCL.Objects.DestinyMembershipType.Xbox },
                new DestinyPCL.Objects.BungieUser() {GamerTag = "jmasternomad", type= DestinyPCL.Objects.DestinyMembershipType.Xbox },
                new DestinyPCL.Objects.BungieUser() {GamerTag = "jmasternomad", type= DestinyPCL.Objects.DestinyMembershipType.Xbox },
                new DestinyPCL.Objects.BungieUser() {GamerTag = "Ed darkninja", type= DestinyPCL.Objects.DestinyMembershipType.Xbox }
            };
            var resultado = api.getPlayersLoop(users);
            foreach (var item in resultado)
            {
                Assert.IsNotNull(item);
            }
            Assert.IsTrue(resultado.Count() == 4,"API returns only Distinct, doesn't call twice for the same User");
        }

        [TestMethod]
        public void MainApi_GetClanWorks()
        {
            var player = api.getPlayerAsync(new DestinyPCL.Objects.BungieUser() { GamerTag = "Dattowatto", type = DestinyPCL.Objects.DestinyMembershipType.PSN }).Result;
            var clan = api.GetPlayerClan(player).Result;

            Assert.IsNotNull(clan);
            Assert.IsNotNull(clan.ClanName);
            Assert.IsNotNull(clan.ClanInfo);
            Assert.IsNotNull(clan.ClanMotto);
            //Assert.IsTrue(clan.ClanMotto == "We've awoken the Hive... And we're not sorry");
            Assert.IsNotNull(clan.Players);
            Assert.IsTrue(clan.Players.Count == clan.MembersCount);
            if (clan.Players.Count > 0)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(clan.Players.First().GamerTag));
            }
        }
    }
}
