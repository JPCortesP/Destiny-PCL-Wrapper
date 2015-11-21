using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web;

namespace Web.ManifestProcessing.Models
{
    public class Manifest
    {
        private static string manifestFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data.content");
        private static string ManifestDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public List<ManifestTable> Tables { get; set; }

        private Manifest(List<ManifestTable> _tables)
        {
            this.Tables = _tables;
            //https://bungie.net/common/destiny_content/sqlite/en/world_sql_content_87546da386887a26a50621a84eab1548.content

        }
        public static Manifest Create(bool TryToUpdateIfExists = false)
        {
            if (!File.Exists(manifestFile))
            {
                downloadManifest();
            }
            else if (TryToUpdateIfExists)
            {
                downloadManifest();
            }
            if (!File.Exists(manifestFile))
                throw new InvalidOperationException("The Manifest file doesn't exist, and couldn't be downloaded");

            List<ManifestTable> tablas = loadManifestData(manifestFile);
            return new Manifest(tablas);
        }

        public dynamic GetItemData(string itemHash)
        {
            if (itemHash != null)
            {
                var table = (from ex in Tables
                             where ex.TableName == "DestinyInventoryItemDefinition"
                             select ex).First();
                var item = from ex in table.Rows
                           where ex.id.ToString() == itemHash
                           select ex;

                return JObject.Parse(item.First().Json);
            }
            else
                return null;

        }
        public dynamic getBucketData(string bucketHash)
        {
            var table = (from ex in Tables
                         where ex.TableName == "DestinyInventoryBucketDefinition"
                         select ex).First();
            var item = from ex in table.Rows
                       where ex.id.ToString() == bucketHash
                       select ex;

            return JObject.Parse(item.First().Json);
        }
        public dynamic getStatsData(string statHash)
        {
            if (statHash == null)
            {
                return null;
            }
            var table = (from ex in Tables
                         where ex.TableName == "DestinyStatDefinition"
                         select ex).First();
            var item = from ex in table.Rows
                       where ex.id.ToString() == statHash
                       select ex;
            return JObject.Parse(item.First().Json);
        }

        private static List<ManifestTable> loadManifestData(string manifestFile)
        {
            List<ManifestTable> Lista = new List<ManifestTable>();
            using (var sqConn = new SQLiteConnection("Data Source=" + manifestFile + ";Version=3;"))
            {
                sqConn.Open();
                //build a list of all the tables
                List<string> tableNames = new List<string>();
                using (var sqCmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table'", sqConn))
                {
                    using (var sqReader = sqCmd.ExecuteReader())
                    {
                        while (sqReader.Read())
                            tableNames.Add((string)sqReader["name"]);
                    }
                }
                //creamos los objetos Tabla
                foreach (var tableName in tableNames)
                {
                    string idKey = "id";
                    if (tableName == "DestinyHistoricalStatsDefinition")
                        idKey = "Key";

                    ManifestTable table = new ManifestTable() { TableName = tableName, Rows = new List<ManifestRow>() };
                    using (var sqCmd = new SQLiteCommand("SELECT * FROM " + tableName, sqConn))
                    {
                        using (var sqReader = sqCmd.ExecuteReader())
                        {
                            while (sqReader.Read())
                            {
                                byte[] jsonData = (byte[])sqReader["json"];
                                string jsonString = Encoding.UTF8.GetString(jsonData);

                                if (idKey == "Key")
                                {
                                    string idData = (string)sqReader[idKey];
                                    table.Rows.Add(new ManifestRow() { id = idData, Json = jsonString });
                                }
                                else
                                {
                                    long idData = (long)sqReader[idKey];
                                    table.Rows.Add(new ManifestRow() { id = ((UInt32)idData).ToString(), Json = jsonString });
                                }

                            }
                        }
                    }
                    Lista.Add(table);

                }
            }
            return Lista;
        }

        private static void downloadManifest()
        {
            using (var hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.Add("X-API-Key", "6def2424db3a4a8db1cef0a2c3a7807e");
                var initialAnswer = hc.GetStringAsync("http://www.bungie.net/platform/destiny/manifest/").Result;
                dynamic jsonInitialAnswer = JObject.Parse(initialAnswer);
                var path = (string)jsonInitialAnswer.Response.mobileWorldContentPaths.en.Value;
                Uri url = new Uri("https://bungie.net/" + path);
                var fileStream = hc.GetStreamAsync("https://bungie.net/" + path).Result;
                var compresedLocalFileStream = File.Create(ManifestDirectory + "\\Temp.zip");
                fileStream.CopyTo(compresedLocalFileStream);
                compresedLocalFileStream.Close();
                FileInfo fileToDecompress = new FileInfo(ManifestDirectory + "\\Temp.zip");
                if (File.Exists(fileToDecompress.DirectoryName + "\\" + Path.GetFileName(url.AbsolutePath)))
                {
                    File.Delete(fileToDecompress.DirectoryName + "\\" + Path.GetFileName(url.AbsolutePath));
                }
                ZipFile.ExtractToDirectory(fileToDecompress.FullName, fileToDecompress.DirectoryName);

                FileInfo newFile = new FileInfo(ManifestDirectory + "\\" + Path.GetFileName(url.AbsolutePath));
                if (File.Exists(manifestFile))
                    File.Delete(manifestFile);
                File.Copy(newFile.FullName, manifestFile);
                File.Delete(newFile.FullName);
                File.Delete(ManifestDirectory + "\\Temp.zip");
            }
        }
    }


    public class ManifestTable
    {
        public string TableName { get; set; }
        public List<ManifestRow> Rows { get; set; }

    }
    public class ManifestRow
    {
        public string id { get; set; }
        public string Json { get; set; }
        public dynamic Data
        {
            get
            {
                return JObject.Parse(Json);
            }
        }
    }
}