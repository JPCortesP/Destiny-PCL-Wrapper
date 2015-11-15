using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace DestinyAPI.Test
{
    [TestClass]
    public class DestinyApiTests
    {
        [TestMethod]
        public async void ReturnsPlayerOrNotFound()
        {
            DestinyAPI api = new DestinyAPI();
            BungieUser user = new BungieUser() { GamerTag = "JPCortesP", type = MembershipType.PSN };//Not found
            BungieUser user1 = new BungieUser() { GamerTag = "JPCortesP", type = MembershipType.Xbox };//Correct

            var Player = await api.GetPlayer(user);
            Assert.IsInstanceOfType(Player, typeof(Player));

            //Not Found
            try
            {
                var OtroPlayer = await api.GetPlayer(user1);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(NotFoundException));
            }
            
            
        }
    }
    
}
