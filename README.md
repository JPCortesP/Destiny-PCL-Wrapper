# DestinyAPI C# Library
Testing Project for a Destiny API Wrapper in C#

Still in development, but no breaking changes are expected. 

You can use the Library to query the Destiny API for the following information:

+ Player information 
+ Characters Race, Level, Class and Gender. Also, the emblem is present. 
+ Inventory, including DB information (must be done manually for now, see below)

Planned up next:
+ Improve Inventory (ie, making it transparent to get item's details)

## Usage
 
```c#
            //Very simple console app, loading characters and items
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
```
###Notes:
+ DestinyAPI class is not tied to any player, it means you can load any player and reuse the object, as the only internal data it holds its the API KEY
+ It uses the Account/Items endpoint to load the data; it means it will load all the items associated but not the grimoire score and clan information. Later on I would try to add another call to fill that information, or maybe even change the calling steps to get items, but for now, I rather have a few calls and full inventory load in just 2 calls instead of 3
