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

namespace DestinyAPI
{
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
    //public partial class Manifest
    //{

    //    public List<ManifestTable> Tables { get; set; }

    //    private Manifest(List<ManifestTable> _tables)
    //    {
    //        this.Tables = _tables;
    //        //https://bungie.net/common/destiny_content/sqlite/en/world_sql_content_87546da386887a26a50621a84eab1548.content

    //    }


    //    public dynamic GetItemData(string itemHash)
    //    {
    //        if (itemHash != null)
    //        {
    //            var table = (from ex in Tables
    //                         where ex.TableName == "DestinyInventoryItemDefinition"
    //                         select ex).First();
    //            var item = from ex in table.Rows
    //                       where ex.id.ToString() == itemHash
    //                       select ex;

    //            return JObject.Parse(item.First().Json);
    //        }
    //        else
    //            return null;

    //    }
    //    public dynamic getBucketData(string bucketHash)
    //    {
    //        var table = (from ex in Tables
    //                     where ex.TableName == "DestinyInventoryBucketDefinition"
    //                     select ex).First();
    //        var item = from ex in table.Rows
    //                   where ex.id.ToString() == bucketHash
    //                   select ex;

    //        return JObject.Parse(item.First().Json);
    //    }
    //    public dynamic getStatsData(string statHash)
    //    {
    //        if (statHash == null)
    //        {
    //            return null;
    //        }
    //        var table = (from ex in Tables
    //                     where ex.TableName == "DestinyStatDefinition"
    //                     select ex).First();
    //        var item = from ex in table.Rows
    //                   where ex.id.ToString() == statHash
    //                   select ex;
    //        return JObject.Parse(item.First().Json);
    //    }


    //    public static Manifest Create(bool TryToUpdateIfExists = false)
    //    {
    //        try
    //        {
    //            var resultado = downloadManifest().Result;
    //            if (!resultado)
    //                throw new InvalidOperationException("The Manifest file doesn't exist, and couldn't be downloaded");

    //            //if (!File.Exists(manifestFile))
    //            //{
    //            //    var resultado = await downloadManifest();
    //            //}
    //            //else if (TryToUpdateIfExists)
    //            //{
    //            //    var resultado = await downloadManifest();
    //            //}
    //            //if (!File.Exists(manifestFile))


    //            List<ManifestTable> tablas = loadManifestData("").Result;
    //            return new Manifest(tablas);
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //            throw new InvalidOperationException("The Manifest file doesn't exist, and couldn't be downloaded", ex);
    //        }
    //    }

    //}


}