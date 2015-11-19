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
using System.Threading.Tasks;

namespace ManifestConverter
{
    class Program
    {
        private static string manifestFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data.content");
        private static string ManifestDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static void Main(string[] args)
        {
            downloadManifest();
            var db = loadAndSaveManifestData();
            saveDb(db);
        }

        private static void saveDb(List<ManifestTable> db)
        {
            var json =  Newtonsoft.Json.JsonConvert.SerializeObject(db);
            File.WriteAllText(ManifestDirectory + "/database.json", json);
        }

        private static List<ManifestTable> loadAndSaveManifestData()
        {
            List<ManifestTable> db = new List<ManifestTable>();
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

                foreach (var tabla in tableNames)
                {
                    string idKey = "id";
                    var Table = new ManifestTable() { Name = tabla, Data = new List<ManifestRow>() };
                    if (tabla == "DestinyHistoricalStatsDefinition")
                    {
                        idKey = "key";
                    }
                    using (var sqCmd = new SQLiteCommand("SELECT * FROM " + tabla, sqConn))
                    {
                        using (var sqReader = sqCmd.ExecuteReader())
                        {
                            while (sqReader.Read())
                            {
                                string idData = "";
                                if (idKey == "id")
                                {
                                    idData = ((UInt32)(long)sqReader[0]).ToString();
                                }
                                else
                                {
                                    idData = sqReader[0].ToString();
                                }
                                byte[] jsonData = (byte[])sqReader[1];
                                string jsonString = Encoding.UTF8.GetString(jsonData);
                                Table.Data.Add(new ManifestRow()
                                {
                                    id = idData.ToString(),
                                    json = jsonString
                                });
                            }
                        }
                    }
                    db.Add(Table);
                }


            }
            return db;
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
}
