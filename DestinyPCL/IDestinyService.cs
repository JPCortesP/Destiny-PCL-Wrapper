using DestinyPCL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL
{
    public interface IDestnyService : IDisposable
    {

        string ApiKey { get; set; }
        Task<DestinyPlayer> getPlayerAsync(BungieUser user);
        Task<List<DestinyPlayer>> getPlayersAsync(List<BungieUser> users);
        IEnumerable<DestinyPlayer> getPlayersLoop(List<BungieUser> users);

        Task<DestinyClan> GetPlayerClan(DestinyPlayer player);
        Task<DestinyItemBase> getInventory(BungieUser user);
        Task<List<Object>> getHistory(BungieUser user);
        Task<bool> EquipItem(DestinyCharacter target, DestinyItemBase item);
        Task<bool> TransferItem(DestinyCharacter target, DestinyItemBase item, bool toVault = false);
        DestinyManifest Manifest { get; set; }
        Task<bool> LoadManifest();
        string ManifestTypeName { get; }


    }
}
