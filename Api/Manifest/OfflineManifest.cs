using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL.Manifest
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

            var file = "DestinyPCL.Manifest.db.";
            switch (table)
            {
                case ManifestTable.InventoryItem:
                    file += "InventoryItem.json";
                    break;
                case ManifestTable.Activity:
                    file += "Activity.json";
                    break;
                case ManifestTable.Gender:
                    file += "Gender.json";
                    break;
                case ManifestTable.InventoryBucket:
                    file += "InventoryBucket.json";
                    break;
                case ManifestTable.Race:
                    file += "Race.json";
                    break;
                case ManifestTable.Stat:
                    file += "Stat.json";
                    break;
                case ManifestTable.Class:
                    file += "Class.json";
                    break;
                default:
                    return null;
            }
            var assembly = typeof(OfflineManifest).GetTypeInfo().Assembly;
            string[] archivos = assembly.GetManifestResourceNames();
            var stream = assembly.GetManifestResourceStream(file);
            var sr = new StreamReader(stream);
            var resultado = sr.ReadToEnd();
            dynamic objeto = JObject.Parse(resultado);
            var algo = objeto;
            dynamic respuesta = objeto[hash];
            return respuesta;
            
        }

        public async Task<dynamic> getDataAsync(ManifestTable table, string hash)
        {
            var file = "Api.Manifest.db.";
            switch (table)
            {
                case ManifestTable.InventoryItem:
                    file += "InventoryItem.json";
                    break;
                case ManifestTable.Activity:
                    file += "Activity.json";
                    break;
                case ManifestTable.Gender:
                    file += "Gender.json";
                    break;
                case ManifestTable.InventoryBucket:
                    file += "InventoryBucket.json";
                    break;
                case ManifestTable.Race:
                    file += "Race.json";
                    break;
                case ManifestTable.Stat:
                    file += "Stat.json";
                    break;
                case ManifestTable.Class:
                    file += "Class.json";
                    break;
                default:
                    return null;
            }
            var assembly = typeof(OfflineManifest).GetTypeInfo().Assembly;
            string[] archivos = assembly.GetManifestResourceNames();
            var stream = assembly.GetManifestResourceStream(file);
            var sr = new StreamReader(stream);
            var resultado = await sr.ReadToEndAsync();
            dynamic objeto = JObject.Parse(resultado);
            var algo = objeto;
            dynamic respuesta = objeto[hash];
            return respuesta;
        }
    }
}
