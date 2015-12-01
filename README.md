# DestinyPCL-Client C# Library
Testing Project for a Destiny PCL API Wrapper in C#

Still in development, breaking changes are expected. 

[![Build status](https://ci.appveyor.com/api/projects/status/sah3i0l5ce1ynd18/branch/master?svg=true)](https://ci.appveyor.com/project/JPCortesP/destinypcl-client/branch/master)
## Usage

```csharp
DestinyService ds = new DestinyService(new DestinyPCL.Manifest.OnlineManifest(), "YOUR API KEY");
var Player = await ds.getPlayerAsync(new DestinyPCL.Objects.BungieUser("JPCortesP", DestinyPCL.Objects.DestinyMembershipType.Xbox));
foreach (var character in Player.Characters)
{
    Console.WriteLine("{0} - {1}", character.Class, character.LightLevel);
}
```


## How to get it
- Daily Builds, on Nugget. Add https://ci.appveyor.com/nuget/destinypcl-client-pwf5n0g84w76 as a source.
