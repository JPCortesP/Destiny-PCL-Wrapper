using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DestinyPCL;

namespace DestinyWeb.Models
{
    public static class DestinyAPI
    {
        private static readonly string apikey = "6def2424db3a4a8db1cef0a2c3a7807e";
        private static readonly DestinyManifest manifest = new DestinyPCL.Manifest.OfflineManifest();

        public static DestinyPCL.DestinyService getApiClient()
        {
            return new DestinyPCL.DestinyService(manifest, apikey);
        }
    }
}