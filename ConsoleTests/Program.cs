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
            int charindex = 0;
            foreach (var personaje in Player.Characters)
            {
                Console.WriteLine("*****************************");
                Console.WriteLine(personaje.Class + " " + personaje.Race + " " + personaje.Gender);
                Console.WriteLine("Nivel: " + personaje.BaseLevel + " Luz: " + personaje.LightLevel);
                Console.WriteLine("ITEMS");
                foreach (var item in Player.Items.Where(g=>g.characterIndex == charindex).ToList() )
                {
                    var info = api.DestinyData.GetItemData(item.itemHash);
                    Console.WriteLine("\t Name: " + info.itemName);
                    Console.WriteLine("\t\t" + info.itemDescription);
                }

                charindex++;
            }
            Console.ReadLine();

        }

        
    }
}
