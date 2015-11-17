using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private StackPanel[] paneles = new StackPanel[4];
        private ListView[] listviews = new ListView[4];
        public MainWindow()
        {
            InitializeComponent();
            paneles[0] = char1;
            paneles[1] = char2;
            paneles[2] = char3;
            paneles[3] = char4; // Vault

            listviews[0] = lv_1;
            listviews[1] = lv_2;
            listviews[2] = lv_3;
            listviews[3] = lv_4;
            cb_platform.SelectedIndex = 0;


        }

        private async void initPlayerInfo(string gamertag, bool isXbox, CookieContainer cookies = null)
        {
            player = await api.GetPlayer(new DestinyAPI.BungieUser()
            { GamerTag = gamertag, type = isXbox ? DestinyAPI.MembershipType.Xbox : DestinyAPI.MembershipType.PSN, cookies = cookies });
            if (player == null)
            {
                MessageBox.Show("GamerTag/Platform combination not found in Bungie");
            }
            else
            {
                this.DataContext = player;
                for (int i = 0; i < player.Characters.Count; i++)
                {
                    paneles[i].DataContext = player.Characters[i];
                    listviews[i].DataContext = player.Characters[i].Items;
                }
                listviews[3].DataContext = player.Items.Where(g => g.characterIndex == -1).ToList();

            }
            


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txt_gt.Text) || cb_platform.SelectedValue == null)
            {
                MessageBox.Show("Gamertag and Platorm are required. ");
            }
            else
            {
                var valor = (cb_publicAPI.IsChecked.HasValue ? cb_publicAPI.IsChecked.Value : true);
                if (!valor)
                {
                    LoginWindo login = new LoginWindo();
                    login.ShowDialog();
                    if (login.Resultado)
                    {
                        var cookiecontainer = login.cookies;

                        initPlayerInfo(txt_gt.Text, cb_platform.Text == "Xbox", cookiecontainer);
                    }
                }
                else
                {
                    initPlayerInfo(txt_gt.Text, cb_platform.Text == "Xbox");
                }

            }
        }
    }
}