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
        [STAThread]
        static void Main(string[] args)
        {
            var loginWindo = new DestinyWeaponExplorer.LoginWindow();
            loginWindo.ShowDialog();
            var user = new BungieUser() { GamerTag = "jpcortesp", type = DestinyMembershipType.Xbox };
            if (loginWindo.Resultado)
            {
                user.cookies = loginWindo.cookies;
            }
            var api = new DestinyPCL.DestinyService(new Win32Manifest(), "6def2424db3a4a8db1cef0a2c3a7807e");
            var player = api.getPlayerAsync(user).Result;

            var ItemTypes = player.Gear.Select(g => g.itemTypeName).Distinct();
            foreach (var type in ItemTypes)
            {
                Console.WriteLine("Top {0}s", type);
                Console.WriteLine("=====================");
                foreach (var item in player.Gear.Where(g=>g.itemTypeName == type).OrderByDescending(h=>h.primaryStats_value).Take(2))
                {
                    Console.WriteLine("{0} - {1}", item.itemName, item.primaryStats_value);
                }
                Console.WriteLine("{0} more {1}", player.Gear.Where(g=>g.itemTypeName == type).Count() -2, type);
                Console.WriteLine("");
            }

            Console.ReadLine();

            //var player = new Player(new object(), new object());
            //Console.Write(player.Items.Count);

            
            

        }
    }
   
}
