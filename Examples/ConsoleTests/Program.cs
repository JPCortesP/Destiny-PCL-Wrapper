using DestinyPCL.Objects;
using DestinyPCL.Win32Manifest;
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
            var api = new DestinyPCL.DestinyService(new Win32Manifest(), "6def2424db3a4a8db1cef0a2c3a7807e");
            var player = api.getPlayerAsync(new BungieUser() { GamerTag = "jpcortesp", type = DestinyMembershipType.Xbox }).Result;

            foreach (var item in player.Gear)
            {
                Console.WriteLine("({0} / {1}) - {2} - {3}", item.state, item.transferStatus, item.itemTypeName, item.itemName);
            }
            foreach (var item in player.Characters)
            {
                Console.WriteLine("{0} - {1} ({2})", item.Class, item.LightLevel, item.BaseLevel);
                Console.WriteLine(item.Race + " " + item.Gender);
            }



            //var player = new Player(new object(), new object());
            //Console.Write(player.Items.Count);

            Console.ReadLine();
            

        }
    }
   
}
