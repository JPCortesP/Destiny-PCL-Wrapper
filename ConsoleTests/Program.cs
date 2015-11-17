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
            var items = Player.Items.Select(g => g.itemTypeName).Distinct().ToList();
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();

            var ListaFrutas = new List<fruta>();
            //agrego algunas frutas, incluidas algunas naranjas
            ListaFrutas.Add(new naranja() { acidez = 10 });
            ListaFrutas.Add(new fruta());
            ListaFrutas.Add(new naranja() { acidez = 7 });
            ListaFrutas.Add(new naranja() { acidez = 4 });
            //LIsta ordenada por acidez, solo naranjas:
            List<naranja> naranjas = ListaFrutas
                .Where(t => t.GetType() == typeof(naranja))
                .Cast<naranja>()
                .OrderByDescending(g => g.acidez)
                .ToList();
            var ListaNaranjas = ListaFrutas.OfType<naranja>().OrderByDescending(n => n.acidez);
        }
    }
    public class fruta { }
    public class naranja : fruta
    {
        public int acidez { get; set; }
    }
}
