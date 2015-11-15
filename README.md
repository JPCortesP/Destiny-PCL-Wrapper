# DestinyAPI C# Library
Testing Project for a Destiny API Wrapper in C#

Still in development, but no breaking changes are expected. 

You can use the Library to query the Destiny API for the following information:

+ Player information (grimoire, clan name)
+ Characters Race, Level, Class and Gender. Also, the emblem is present. 

Planned up next:
+ Inventory

## Usage
 
```c#
DestinyAPI api = new DestinyAPI();
BungieUser user1 = new BungieUser() { GamerTag = "Your Gamertag", type = MembershipType.Xbox };
var Player = await api.GetPlayer(user1):
```
