using DestinyPCL.Objects;
using DestinyPCL.Win32Manifest;
using System;
using System.Linq;

namespace ConsoleTests
{
    class Program
    {
        
        [STAThread]
        static void Main(string[] args)
        {
            var LoginWindow = new DestinyWeaponExplorer.LoginWindow();
            LoginWindow.ShowDialog();
            var user = new BungieUser() { GamerTag = "jpcortesp", type = DestinyMembershipType.Xbox };
            if (LoginWindow.Resultado)
            {
                user.cookies = LoginWindow.cookies;
            }
            var api = new DestinyPCL.DestinyService(new Win32Manifest(), "6def2424db3a4a8db1cef0a2c3a7807e");
            var player = api.getPlayerAsync(user).Result;
            player.BestGearbyBucket(player.Characters.First());
            var ItemTypes = player.Gear.Select(g => g.itemTypeName).Distinct();
            //ALL THE GEAR ORDERED AND SHOWING THE BEST OF ANY TYPE.
            //=================================================================
            foreach (var type in ItemTypes)
            {
                Console.WriteLine("Top {0}s", type);
                Console.WriteLine("=====================");
                foreach (var item in player.Gear.Where(g => g.itemTypeName == type).OrderByDescending(h => h.primaryStats_value).Take(2))
                {
                    Console.WriteLine("{0} - {1} - {2}", item.itemName, item.primaryStats_value, item.dbData?.classType);

                }
                Console.WriteLine("{0} more {1}", player.Gear.Where(g => g.itemTypeName == type).Count() - 2, type);
                Console.WriteLine("");
            }

            //BEST GEAR IN YEAR 2 PER CHARACTER, TAKING INTO ACCOUNT IF CAN BE EQUIPPED. 
            var buckets = player.Gear.Select(g => g.bucketData?.bucketName?.Value).Distinct().ToList();
            foreach (var cara in player.Characters)
            {
                Console.WriteLine("");
                Console.WriteLine(cara.Class);
                Console.WriteLine("===========");
                foreach (var bucket in buckets)
                {
                    var item = player.Gear
                        .Where(g => g.bucketData?.bucketName?.Value == bucket)
                        .OrderByDescending(g => g.primaryStats_value)
                        .Where(g => g.canBeEquippedOn(cara.ClassIdInt) == true)
                        .First();
                    Console.WriteLine("{2}: {0} - {1}", item.itemName, item.primaryStats_value, bucket);
                }
                
            }

            Console.ReadLine();
            

            
            

        }
    }
   
}
