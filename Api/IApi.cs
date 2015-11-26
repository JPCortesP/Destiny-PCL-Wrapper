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
        Task<Player> getPlayerAsync(BungieUser user);
        Task<List<Player>> getPlayersAsync(List<BungieUser> users);
        Task<List<Player>> getAllPlayersInClan(BungieUser user);
        Task<ItemBase> getInventory(BungieUser user);
        Task<List<Object>> getHistory(BungieUser user);
        Task<bool> EquipItem(Character target, ItemBase item);
        Task<bool> TransferItem(Character target, ItemBase item, bool toVault = false);
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
