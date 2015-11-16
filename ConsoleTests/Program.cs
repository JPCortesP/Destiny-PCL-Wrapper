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
            Console.Write("Gamertag (XBOX Only): ");
            var gt = Console.ReadLine();
            Console.WriteLine("Loading");
            DestinyAPI.DestinyAPI api = new DestinyAPI.DestinyAPI();
            var Player = api.GetPlayer(new DestinyAPI.BungieUser()
            { GamerTag = gt, type = DestinyAPI.MembershipType.Xbox }).Result;
            Console.Clear();
            Console.WriteLine(Player.GamerTag);
            Console.WriteLine("===================");
            var items = Player.Items.OrderByDescending(f => f.itemTypeName).ToList();
            foreach (var item in items)
            {
                Console.WriteLine(
                    string.Format( "{0} - {1}", item.itemTypeName, item.itemName)
                    );
            }
            
            Console.ReadLine();

        }

        
    }
}
