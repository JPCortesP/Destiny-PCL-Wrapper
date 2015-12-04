using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace JPDestiny
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DestinyPCL.DestinyService API = new DestinyPCL.DestinyService(new DestinyPCL.Manifest.OnlineManifest(), "6def2424db3a4a8db1cef0a2c3a7807e");
        public static DestinyPCL.Objects.DestinyPlayer Player;
    }
}
