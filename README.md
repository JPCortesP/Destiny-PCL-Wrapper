# DestinyPCL-Client C# Library
Testing Project for a Destiny PCL API Wrapper in C#

Still in development, breaking changes are expected. 

[![Build status](https://ci.appveyor.com/api/projects/status/sah3i0l5ce1ynd18/branch/master?svg=true)](https://ci.appveyor.com/project/JPCortesP/destinypcl-client/branch/master)
## Usage

```csharp
DestinyService ds = new DestinyService(new DestinyPCL.Manifest.OnlineManifest(), "YOUR API KEY");
var Player = await ds.getPlayerAsync(new DestinyPCL.Objects.BungieUser("GamerTag / PSN ID", DestinyPCL.Objects.DestinyMembershipType.Xbox));
foreach (var character in Player.Characters)
{
    Console.WriteLine("{0} - {1}", character.Class, character.LightLevel);
}
```
### Authenticated Request.
For any Auth Request, you must capture yourself the Cookies. For it, there's some help in DestinyPCL.AuthHelpers:
```csharp
//Get the PSN Login URL
public static readonly string PSNLoginUrl
//Get the Xbox Login URL
public static readonly string XboxLoginUrl;
//Array of all the Required Cookie Names to be in the Coockie Container
public static readonly string[] RequiredCookieNames;

//Wanna Check if your CookieContainer has what it takes? use this function. 
public static bool CheckForRequiredCookies(CookieContainer cookies);
```
then use that Cookie Container inside a new (or existent) BungieUser object and call the APIs.

There's some examples in the Repo or in the Wiki. Check them out. 

### Get an Api KEY
from https://www.bungie.net/en/Clan/Post/39966/85087279/0/0 instructions, go to https://www.bungie.net/en/User/API. 

## How to get it
- Daily Builds, on Nugget. Add https://ci.appveyor.com/nuget/destinypcl-client-pwf5n0g84w76 as a source. This nugget package feed will be updated everytime a new commit is done on master. I'll try to avoid to push to master any non-working version.
