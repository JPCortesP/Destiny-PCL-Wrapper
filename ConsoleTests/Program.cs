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
            //var Manifest = new Api.Manifest.OnlineManifest();
            //var api = new Api.API(Manifest, "6def2424db3a4a8db1cef0a2c3a7807e");
            //var player = api.getPlayerAsync(new Api.Objects.BungieUser() { GamerTag = "jpcortesp", type = Api.Objects.MembershipType.Xbox }).Result;

            //foreach (var item in player.Gear)
            //{
            //    Console.WriteLine("({0} / {1}) - {2}", item.state, item.transferStatus, item.itemTypeName);
            //}
            //foreach (var item in player.Characters)
            //{
            //    Console.WriteLine("{0} - {1} ({2})",item.Class, item.LightLevel, item.BaseLevel);
            //    Console.WriteLine(item.Race + " " + item.Gender);
            //}

            var player = new Player(new object(), new object());
            Console.Write(player.Items.Count);
            
            Console.ReadLine();
            

        }
    }
   
}
