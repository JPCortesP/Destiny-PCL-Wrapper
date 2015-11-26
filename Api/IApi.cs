
using DestinyPCL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL
{
    public interface IDestnyService : IDisposable
    {
        
        string ApiKey { get; set; }
        Task<DestinyPlayer> getPlayerAsync(BungieUser user);
        Task<List<DestinyPlayer>> getPlayersAsync(List<BungieUser> users);
        Task<DestinyClan> getAllPlayersInClan(BungieUser user);
        Task<DestinyItemBase> getInventory(BungieUser user);
        Task<List<Object>> getHistory(BungieUser user);
        Task<bool> EquipItem(DestinyCharacter target, DestinyItemBase item);
        Task<bool> TransferItem(DestinyCharacter target, DestinyItemBase item, bool toVault = false);
        DestinyManifest Manifest { get; set; }
        Task<bool> LoadManifest(DestinyManifest instance);
        string ManifestTypeName { get; }
        

    }

    public interface DestinyManifest : IDisposable
    {
        string ApiKey { get; set; }
        dynamic getData(Manifest.ManifestTable table, string hash);
        Task<dynamic> getDataAsync(Manifest.ManifestTable table, string hash);
    }
    
}
