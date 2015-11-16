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

        }

        private async void initPlayerInfo(string gamertag, bool isXbox)
        {
            player = await api.GetPlayer(new DestinyAPI.BungieUser()
            { GamerTag = "JPCortesP", type = DestinyAPI.MembershipType.Xbox });
            if (player == null)
            {
                MessageBox.Show("GamerTag/Platform combination not found in Bungie");
            }
            else
                this.DataContext = player;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var valor = (cb_publicAPI.IsChecked.HasValue ? cb_publicAPI.IsChecked.Value : true);
            if (! valor )
            {
                LoginWindo login = new LoginWindo();
                login.ShowDialog();
                if (login.Resultado)
                {
                    var cookiecontainer = login.cookies;
                    api = new DestinyAPI.DestinyAPI(cookies: cookiecontainer);
                    initPlayerInfo(txt_gt.Text, cb_platform.Text == "Xbox");
                }
            }
            else
            {
                initPlayerInfo(txt_gt.Text, cb_platform.Text == "Xbox");
            }

        }
    }
}
