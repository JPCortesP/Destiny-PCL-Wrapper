using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyPCL.Objects;

namespace DestinyPCL
{
    public partial class DestinyService : IDestinyService
    {
        public Task<List<object>> getHistoricalActivities(BungieUser user, DestinyCharacter character)
        {
            throw new NotImplementedException();
        }
        public Task<List<object>> getHistoricalActivities(BungieUser user, DestinyPlayer player)
        {
            throw new NotImplementedException();
        }
        public Task<List<Object>> getAgregatedStats(BungieUser user, DestinyCharacter character)
        {
            throw new NotImplementedException();
        }
        public Task<List<Object>> getAgregatedStats(BungieUser user, DestinyPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
