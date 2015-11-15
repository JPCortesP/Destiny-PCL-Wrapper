# DestinyAPI C# Library
Testing Project for a Destiny API Wrapper in C#

Still in development, but no breaking changes are expected. 

You can use the Library to query the Destiny API for the following information:

+ Player information 
+ Characters Race, Level, Class and Gender. Also, the emblem is present. 

Planned up next:
+ Inventory

## Usage
 
```c#
DestinyAPI api = new DestinyAPI("YOUR API KEY");
BungieUser user1 = new BungieUser() { GamerTag = "Your Gamertag", type = MembershipType.Xbox };
var Player = await api.GetPlayer(user1):
```
###Notes:
+ DestinyAPI class is not tied to any player, it means you can load any player and reuse the object, as the only internal data it holds its the API KEY
+ It uses the Account/Items endpoint to load the data; it means it will load all the items associated but not the grimoire score and clan information. Later on I would try to add another call to fill that information, or maybe even change the calling steps to get items, but for now, I rather have a few calls and full inventory load in just 2 calls instead of 3
