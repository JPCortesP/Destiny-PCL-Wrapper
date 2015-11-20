using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;

using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DestinyAPI.db
{
    public partial class Manifest
    {
        private static string manifestFile = ""; // Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data.content");
        private static string ManifestDirectory = ""; // Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public List<ManifestTable> Tables { get; set; }

        private Manifest(List<ManifestTable> _tables)
        {
            this.Tables = _tables;
            //https://bungie.net/common/destiny_content/sqlite/en/world_sql_content_87546da386887a26a50621a84eab1548.content

        }
        public static async Task< Manifest> Create(bool TryToUpdateIfExists = false)
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

            List<ManifestTable> tablas = await loadManifestData(manifestFile);
            return new Manifest(tablas) ;
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
        public dynamic getBucketData (string bucketHash)
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
                compresedLocalFileStream.Dispose();
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
        public UInt32 id { get; set; }
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