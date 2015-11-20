using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyAPI
{
     public interface IDestinyAPI
    {
        List<ManifestTable> DestinyData { get; set; }
        Task<bool> LoadManifestData(bool reloadIfExists = false);
        Task<Player> GetPlayer(BungieUser user);
    }
}
