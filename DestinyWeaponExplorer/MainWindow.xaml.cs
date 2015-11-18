using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<string> GearType = new ObservableCollection<string>() { "All" };
        private ObservableCollection<string> GearTier = new ObservableCollection<string>() { "All" };
        public MainWindow()
        {
            InitializeComponent();
            //paneles[0] = char1;
            //paneles[1] = char2;
            //paneles[2] = char3;
            //paneles[3] = char4; // Vault

            //listviews[0] = lv_1;
            //listviews[1] = lv_2;
            //listviews[2] = lv_3;
            //listviews[3] = lv_4;
            cb_platform.SelectedIndex = 0;
            cb_Rarity.ItemsSource = GearTier;
            cb_Type.ItemsSource = GearType;
            


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
                GearType.Clear();
                GearType.Add("All");
                GearTier.Clear();
                GearTier.Add("All");
                cb_Rarity.SelectedIndex = 0;
                cb_Type.SelectedIndex = 0;

                this.DataContext = player;
                var gear = player.Items
                    .Where(t => t.GetType() == typeof(DestinyAPI.ItemGear))
                    .Cast<DestinyAPI.ItemGear>()
                    .OrderByDescending(g => g.primaryStats_value)
                    .ToList();
                this.lv_items.DataContext = gear;
                
                var ListaTipos = gear.Select(g => g.itemTypeName).Distinct().OrderBy(g => g).ToList();
                var ListaRarity = gear.Select(g => g.tierTypeName).Distinct().OrderBy(g => g).ToList();
                foreach (var item in ListaTipos )
                {
                    GearType.Add(item);
                }
                foreach (var item in ListaRarity)
                {
                    GearTier.Add(item);
                }
                


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

        private void cb_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtrar();

        }

        private void Filtrar()
        {
            if (player == null)
                return;
            var filtered = player.Items
                                .Where(t => t.GetType() == typeof(DestinyAPI.ItemGear))
                                .Cast<DestinyAPI.ItemGear>()
                                .OrderByDescending(g => g.primaryStats_value)
                                .ToList();
            if (cb_Type.SelectedItem != null)
            {
                if (cb_Type.SelectedItem.ToString() != "All")
                {
                    filtered = filtered.Where(g => g.itemTypeName == cb_Type.SelectedValue.ToString()).ToList();
                } 
            }
            if (cb_Rarity.SelectedItem != null)
            {
                if (cb_Rarity.SelectedItem.ToString() != "All")
                {
                    filtered = filtered.Where(g => g.tierTypeName == cb_Rarity.SelectedValue.ToString()).ToList();
                } 
            }
            this.lv_items.DataContext = filtered;
        }

        private void cb_Rarity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtrar();

        }
    }
}