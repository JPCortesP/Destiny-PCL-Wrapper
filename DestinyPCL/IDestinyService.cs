using DestinyPCL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL
{
    public interface IDestinyService : IDisposable
    {

        string ApiKey { get; set; }
        Task<DestinyPlayer> getPlayerAsync(BungieUser user);
        Task<List<DestinyPlayer>> getPlayersAsync(List<BungieUser> users);
        IEnumerable<DestinyPlayer> getPlayersLoop(List<BungieUser> users);

        Task<DestinyClan> GetPlayerClan(DestinyPlayer player);
        [Obsolete("Please call getPlayer and use Player.gear")]
        
        Task<List<object>> getHistoricalActivities(BungieUser user, DestinyCharacter character);
        Task<List<object>> getHistoricalActivities(BungieUser user, DestinyPlayer player);
        Task<List<Object>> getAgregatedStats(BungieUser user, DestinyCharacter character);
        Task<List<Object>> getAgregatedStats(BungieUser user, DestinyPlayer player);
        Task<bool> EquipItem(BungieUser AuthUser, DestinyCharacter target, DestinyItemBase item);
        Task<bool> TransferItem(BungieUser AuthUser, DestinyCharacter target, DestinyItemBase item, int quantity = 1, bool toVault = false);
        DestinyManifest Manifest { get; set; }
        Task<bool> LoadManifest();
        string ManifestTypeName { get; }


    }
}
