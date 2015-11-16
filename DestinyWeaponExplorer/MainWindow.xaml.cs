using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DestinyWeaponExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DestinyAPI.DestinyAPI api = new DestinyAPI.DestinyAPI();
        public DestinyAPI.Player player { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            initPlayerInfo();
        }

        private async void initPlayerInfo()
        {
            player = await api.GetPlayer(new DestinyAPI.BungieUser() { GamerTag = "JPCortesP", type = DestinyAPI.MembershipType.Xbox });
            this.DataContext = player;
        }
    }
}
