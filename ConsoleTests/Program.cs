using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var Manifest = new Api.Manifest.OnlineManifest("6def2424db3a4a8db1cef0a2c3a7807e");
            var api = new Api.API(Manifest, "6def2424db3a4a8db1cef0a2c3a7807e");
            var player = api.getPlayerAsync(new Api.Objects.BungieUser() { GamerTag = "jpcortesp", type = Api.Objects.MembershipType.Xbox }).Result;

            foreach (var item in player.Characters)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadLine();
            

        }
    }
   
}
