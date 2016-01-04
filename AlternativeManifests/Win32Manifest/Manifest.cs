using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyPCL.Manifest;
using System.IO;
using System.Reflection;
using System.Net.Http;
using System.IO.Compression;
using System.Data.SQLite;
using Newtonsoft.Json.Linq;

namespace DestinyPCL.Win32Manifest
{
    /// <summary>
    /// MultiLanguage implementation of DestinyManifest. Includes local SQLite DB for better performance.
    /// </summary>
    public class Win32Manifest : DestinyManifest
    {
        public string ApiKey { get; set; }
        public ManifestLanguage CurrentLanguage { get; set; }
        public string FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\manifest.data";
        string CompressedFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\manifest.data.zip";
        string FileFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        object lockObject = new object();


        public Win32Manifest(ManifestLanguage language = ManifestLanguage.en)
        {
            this.CurrentLanguage = language;
        }
        public Win32Manifest(string apikey, ManifestLanguage language = ManifestLanguage.en)
        {
            this.CurrentLanguage = language;
            this.ApiKey = apikey;
        }

        public void Dispose()
        {

        }

        public dynamic getData(ManifestTable table, string hash)
        {
            lock (lockObject)
            {
                if (!DbExists())
                {
                    var algo = this.Preload().Result;
                }
            }

            var ConnString = "Data Source=" + FilePath;
            string json = "";
            using (SQLiteConnection con = new SQLiteConnection(ConnString))
            {
                con.Open();
                var tableNamesQuery = "Select * from " + getRealTableName(table);
                using (SQLiteCommand cmd = new SQLiteCommand(tableNamesQuery, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            UInt32 id = (UInt32)rdr.GetInt32(0);
                            if (id == UInt32.Parse(hash))
                            {
                                json = rdr.GetString(1);
                                break;
                            }
                           

                        }
                    }
                }

            }
            if (string.IsNullOrWhiteSpace(json))
                return null;
            dynamic dinamico = JObject.Parse(json);
            return dinamico;
        }

        public async Task<dynamic> getDataAsync(ManifestTable table, string hash)
        {
            return await getData(table, hash);
        }

        public Task<bool> Preload()
        {
            string url = "http://www.bungie.net/Platform/Destiny/Manifest/";
            using (HttpClient client = new HttpClient())
            {
                string respuesta = GetStringFromBungie(url, client);
                var respuestaTuanis = Newtonsoft.Json.JsonConvert.DeserializeObject<DestinyPCLWin32Manifest.BungieResponses.getManifest.RootObject>(respuesta);
                var ArchivoIdioma = "";
                switch (this.CurrentLanguage)
                {
                    case ManifestLanguage.en:
                        ArchivoIdioma = respuestaTuanis.Response.mobileWorldContentPaths.en;
                        break;
                    case ManifestLanguage.fr:
                        ArchivoIdioma = respuestaTuanis.Response.mobileWorldContentPaths.fr;
                        break;
                    case ManifestLanguage.es:
                        ArchivoIdioma = respuestaTuanis.Response.mobileWorldContentPaths.es;
                        break;
                    case ManifestLanguage.de:
                        ArchivoIdioma = respuestaTuanis.Response.mobileWorldContentPaths.de;
                        break;
                    case ManifestLanguage.it:
                        ArchivoIdioma = respuestaTuanis.Response.mobileWorldContentPaths.it;
                        break;
                    case ManifestLanguage.ja:
                        ArchivoIdioma = respuestaTuanis.Response.mobileWorldContentPaths.ja;
                        break;
                    case ManifestLanguage.pt_br:
                        ArchivoIdioma = respuestaTuanis.Response.mobileWorldContentPaths.pt_br;
                        break;
                    default:
                        ArchivoIdioma = respuestaTuanis.Response.mobileWorldContentPaths.en;
                        break;
                }
                using (var stream = client.GetStreamAsync("https://bungie.net" + ArchivoIdioma).Result)
                {
                    if (File.Exists(CompressedFilePath))
                        File.Delete(CompressedFilePath);

                    using (var file = File.Create(CompressedFilePath))
                    {
                        stream.CopyTo(file);
                        file.Close();
                    }
                }
                //Descomprimir CompressedFilepath to FilePath
                using (ZipArchive archivo = ZipFile.OpenRead(CompressedFilePath))
                {
                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);
                    }
                    archivo.Entries.First().ExtractToFile(FilePath);
                }
                File.Delete(CompressedFilePath);



            }
            return new Task<bool>(() => File.Exists(FilePath));
        }

        private string GetStringFromBungie(string url, HttpClient client)
        {
            if (!client.DefaultRequestHeaders.Contains("x-api-key"))
            {
                client.DefaultRequestHeaders.Add("x-api-key", this.ApiKey);
            }

            return client.GetStringAsync(url).Result;
        }
        private string getRealTableName(ManifestTable table)
        {
            switch (table)
            {
                case ManifestTable.InventoryItem:
                    return "DestinyInventoryItemDefinition";
                case ManifestTable.Activity:
                    return "DestinyActivityDefinition";
                case ManifestTable.Gender:
                    return "DestinyGenderDefinition";
                case ManifestTable.InventoryBucket:
                    return "DestinyInventoryBucketDefinition";
                case ManifestTable.Race:
                    return "DestinyRaceDefinition";
                case ManifestTable.Stat:
                    return "DestinyStatDefinition";
                case ManifestTable.Class:
                    return "DestinyClassDefinition";
                default:
                    throw new NotImplementedException("This table is not yet implemented");
            }
        }
        private bool DbExists()
        {
            return File.Exists(FilePath);
        }
    }


}