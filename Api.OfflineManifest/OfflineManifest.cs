using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.OfflineManifest
{
    public partial class OfflineManifest : DestinyManifest
    {
        public OfflineManifest(string apikey)
        {
            this.ApiKey = apikey;
        }
        public string ApiKey { get; set; }

        public void Dispose()
        {

        }

        public dynamic getData(ManifestTable table, string hash)
        {

            throw new NotImplementedException();
        }
    }
}
