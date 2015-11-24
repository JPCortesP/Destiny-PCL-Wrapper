using Api.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public partial class API : IApi
    {
        public API()
        {

        }
        public string ApiKey { get; set; }
        public DestinyManifest Manifest { get; set; }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public Task<bool> EquipItem(Character target, ItemBase item)
        {
            throw new NotImplementedException();
        }

        public Task<List<object>> getHistory(BungieUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ItemBase> getInventory(BungieUser user)
        {
            throw new NotImplementedException();
        }

        public Task<Player> getPlayerAsync(BungieUser user)
        {
            throw new NotImplementedException();
        }

        public Task<List<Player>> getPlayersAsync(List<BungieUser> users)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoadManifest(DestinyManifest instance)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TransferItem(Character target, ItemBase item, bool toVault = false)
        {
            throw new NotImplementedException();
        }
    }
}
