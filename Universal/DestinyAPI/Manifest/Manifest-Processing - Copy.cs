//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DestinyAPI.db
//{
//    public partial class Manifest
//    {
//        private static List<ManifestTable> loadManifestDataOld(string manifestFile)
//        {
//            List<ManifestTable> Lista = new List<ManifestTable>();
//            using (var sqConn = new SQLiteConnection("Data Source=" + manifestFile + ";Version=3;"))
//            {
//                //sqConn.Open();
//                //build a list of all the tables
//                List<string> tableNames = new List<string>();
//                using (var sqCmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table'", sqConn))
//                {
//                    using (var sqReader = sqCmd.ExecuteReader())
//                    {
//                        while (sqReader.Read())
//                            tableNames.Add((string)sqReader["name"]);
//                    }
//                }
//                //creamos los objetos Tabla
//                foreach (var tableName in tableNames)
//                {
//                    string idKey = "id";
//                    if (tableName != "DestinyHistoricalStatsDefinition")
//                    {

//                        ManifestTable table = new ManifestTable() { TableName = tableName, Rows = new List<ManifestRow>() };
//                        using (var sqCmd = new SQLiteCommand("SELECT * FROM " + tableName, sqConn))
//                        {
//                            using (var sqReader = sqCmd.ExecuteReader())
//                            {
//                                while (sqReader.Read())
//                                {

//                                    long idData = (long)sqReader[idKey];
//                                    byte[] jsonData = (byte[])sqReader["json"];
//                                    string jsonString = Encoding.UTF8.GetString(jsonData);
//                                    table.Rows.Add(new ManifestRow() { id = (UInt32)idData, Json = jsonString });

//                                }
//                            }
//                        }
//                        Lista.Add(table);
//                    }
//                }
//            }
//            return Lista;
//        }
//    }
//}
