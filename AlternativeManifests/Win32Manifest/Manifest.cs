using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyPCL.Manifest;

namespace DestinyPCLWin32Manifest
{
    public class Manifest : DestinyPCL.DestinyManifest
    {
        public string ApiKey        {            get; set;        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public dynamic getData(ManifestTable table, string hash)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> getDataAsync(ManifestTable table, string hash)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Preload()
        {
            throw new NotImplementedException();
        }
    }
}
