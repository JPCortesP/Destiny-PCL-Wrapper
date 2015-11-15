using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


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

            var Player = api.GetPlayer(user1).Result;
            Assert.IsInstanceOfType(Player, typeof(Player));

            //Not Found
                var OtroPlayer = api.GetPlayer(user).Result;
            Assert.IsNull(OtroPlayer);
            
            
        }
    }
    
}
