using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL.Manifest
{

    public sealed partial class OnlineManifest : DestinyManifest
    {
        private string _apikey { get; set; }

        /// <summary>
        /// Stores the API-KEY. Don't update while using.
        /// </summary>
        public string ApiKey
        {
            get
            {
                return _apikey;
            }

            set
            {
                _apikey = value;
                updateKey();
            }
        }

        private void updateKey()
        {
            hc.DefaultRequestHeaders.Add("X-API-Key", _apikey);
        }

        private HttpClient hc = new HttpClient();
        public Dictionary<ManifestTable, string> Tables = new Dictionary<ManifestTable, string>()
        {
            { ManifestTable.InventoryItem,"InventoryItem" },
            { ManifestTable.Activity,"Activity"},
            { ManifestTable.Gender ,"Gender"},
            
            {  ManifestTable.InventoryBucket,"InventoryBucket"},
            
            { ManifestTable.Race ,"Race"},
            { ManifestTable.Stat,"Stat"},
            {ManifestTable.Class, "Class" }

        };
        public OnlineManifest() { }
        public OnlineManifest(string APiKey)
        {
            this._apikey = APiKey;
            updateKey();
        }
        public void Dispose()
        {
            hc.Dispose();
        }

        public dynamic getData(ManifestTable table, string hash)
        {
            var cache = checkCache(table, hash);
            if (cache != null)
            {
                return cache;
            }
            string tabla = convertir(table);
            if (tabla != null)
            {
                var url = String.Format("http://www.bungie.net/Platform/Destiny/Manifest/{0}/{1}/", tabla, hash);
                var resultado = hc.GetStringAsync(url).Result;
                dynamic objeto = JObject.Parse(resultado);
                if (objeto.ErrorStatus == "Success")
                {
                    dynamic response = PrepareReturn(objeto, table);
                    return response;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public async Task<dynamic> getDataAsync(ManifestTable table, string hash)
        {
            var cache = checkCache(table, hash);
            if (cache != null)
            {
                return cache;
            }
            string tabla = convertir(table);
            if (tabla != null)
            {
                var url = String.Format("http://www.bungie.net/Platform/Destiny/Manifest/{0}/{1}/", tabla, hash);
                var resultado = await hc.GetStringAsync(url);
                dynamic objeto = JObject.Parse(resultado);
                if (objeto.ErrorStatus == "Success")
                {
                    dynamic response = PrepareReturn(objeto, table);
                    return response;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private dynamic checkCache(ManifestTable table,string hash)
        {
            switch (table)
            {
                case ManifestTable.Gender:
                    if (hash == "2204441813")
                        return JObject.Parse("{'genderName': 'Female'}");
                    if (hash == "3111576190")
                        return JObject.Parse("{'genderName': 'Male'}");
                    else
                        return null;
                case ManifestTable.Race:
                    if (hash == "2803282938")
                        return JObject.Parse("{'raceName': 'Awoken'}");
                    if (hash == "3887404748")
                        return JObject.Parse("{'raceName': 'Human'}");
                    if (hash == "898834093")
                        return JObject.Parse("{'raceName': 'Exo'}");
                    else
                        return null;
                
                case ManifestTable.Class:
                    if (hash == "2271682572")
                        return JObject.Parse("{'className': 'Warlock'}");
                    if (hash == "3655393761")
                        return JObject.Parse("{'className': 'Titan'}");
                    if (hash == "671679327")
                        return JObject.Parse("{'className': 'Hunter'}");
                    else
                        return null;
                default:
                    return null;
            }
        }

        private string convertir(ManifestTable table)
        {
            try
            {
                return Tables[table];
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        private static dynamic PrepareReturn(dynamic objeto, ManifestTable tabla)
        {
            switch (tabla)
            {
                case ManifestTable.InventoryItem:
                    return (dynamic)objeto.Response.data.inventoryItem;
                case ManifestTable.Activity:
                    return (dynamic)objeto.Response.data.activity;
                case ManifestTable.Gender:
                    throw new NotImplementedException();
                
                case ManifestTable.InventoryBucket:
                    return (dynamic)objeto.Response.data.inventoryBucket;
                
                case ManifestTable.Race:
                    throw new NotImplementedException();
                case ManifestTable.Stat:
                    return (dynamic)objeto.Response.data.stat;
                case ManifestTable.Class:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException("Implemente el resto weon!");
            }

        }

        public Task<bool> Preload()
        {
            return Task.Run(() => true);
        }
    }


    public class HashNotFoundException : Exception
    {
        public HashNotFoundException() { }
        public HashNotFoundException(string message) : base(message) { }
        public HashNotFoundException(string message, Exception inner) : base(message, inner) { }

    }
}
