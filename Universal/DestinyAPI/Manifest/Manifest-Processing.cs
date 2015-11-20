using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyAPI.db
{
    public partial class Manifest
    {
        private static async Task< List<ManifestTable>> loadManifestData(string manifestFile)
        {
            List<ManifestTable> Lista = new List<ManifestTable>();
            var Connection = new SQLiteAsyncConnection("");
            var query = await Connection.ExecuteAsync("SELECT name FROM sqlite_master WHERE type='table'");
            
            

            return Lista;
        }
    }
}
