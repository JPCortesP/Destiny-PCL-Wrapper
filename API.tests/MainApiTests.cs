using Api.Manifest;
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
        private Api.IApi api = new Api.API(new OnlineManifest(), key);
        [TestMethod]
        public void MainApi_ManifestNameWorks()
        {
           
            var name = api.ManifestTypeName;
            Assert.IsNotNull(name);
            Assert.IsTrue(name == "Api.Manifest.OnlineManifest");

        }
        [TestMethod]
        public void MainApi_ReturnsPlayerCorrectly()
        {
            var player = api.getPlayerAsync(new Api.Objects.BungieUser() { GamerTag = "JPCortesP", type = Api.Objects.MembershipType.Xbox }).Result;
            Assert.IsNotNull(player);

            Assert.IsTrue(player.GamerTag == "JPCortesP");
            Assert.IsTrue(player.Characters.Count == 3);
            Assert.IsNotNull(player.Gear);
            Assert.IsNotNull(player.Gear.First().primaryStats_Name);
        }

        [TestMethod]
        public void MainApi_GT_Is_Case_Insensitive()
        {
            var player1 = api.getPlayerAsync(new Api.Objects.BungieUser() { GamerTag = "jpcortesp", type = Api.Objects.MembershipType.Xbox });
            var player2 = api.getPlayerAsync(new Api.Objects.BungieUser() { GamerTag = "JPCortesP", type = Api.Objects.MembershipType.Xbox });

            Assert.AreEqual(player1.Result.MembershipId, player2.Result.MembershipId);
        }
    }
}
