using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL
{
    /// <summary>
    /// DestinyManifest interface. You can implement your own. 
    /// Promises: ApiKey will be inserted before use, always. You Have to call Preload() by yourself, and make it thread safe.
    /// </summary>
    public interface DestinyManifest : IDisposable
    {
        string ApiKey { get; set; }
        dynamic getData(Manifest.ManifestTable table, string hash);
        Task<dynamic> getDataAsync(Manifest.ManifestTable table, string hash);
        Task<bool> Preload();
    }
}
