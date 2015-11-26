
using DestinyPCL.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
        public DestinyPCL.DestinyService api = new DestinyPCL.DestinyService(new DestinyPCL.Manifest.OnlineManifest(), "6def2424db3a4a8db1cef0a2c3a7807e");
        public DestinyPlayer player { get; set; }
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

        private async Task<bool> initPlayerInfo(string gamertag, bool isXbox, CookieContainer cookies = null)
        {
            var cachedPlayer = retrievePlayer(gamertag);
            if (cachedPlayer != null)
            {
                player = cachedPlayer;
            }
            else
            {
                player = await api.getPlayerAsync(new BungieUser()
                { GamerTag = gamertag, type = isXbox ? DestinyMembershipType.Xbox : DestinyMembershipType.PSN, cookies = cookies });
            }
            
            if (player == null)
            {
                MessageBox.Show("GamerTag/Platform combination not found in Bungie");
                return false;
            }
            else
            {
                savePlayer(player);
                GearType.Clear();
                GearType.Add("All");
                GearTier.Clear();
                GearTier.Add("All");
                cb_Rarity.SelectedIndex = 0;
                cb_Type.SelectedIndex = 0;

                this.DataContext = player;
                await Task.Run(() =>
               {
                   var gear = player.Items
                        .Where(t => t.GetType() == typeof(DestinyItemGear))
                        .Cast<DestinyItemGear>()
                        .OrderByDescending(g => g.primaryStats_value)
                        .ToList();
                   this.lv_items.DataContext = gear;

                   var ListaTipos = gear.Select(g => g.itemTypeName).Distinct().OrderBy(g => g).ToList();
                   var ListaRarity = gear.Select(g => g.tierTypeName).Distinct().OrderBy(g => g).ToList();
                   foreach (var item in ListaTipos)
                   {
                       GearType.Add(item);
                   }
                   foreach (var item in ListaRarity)
                   {
                       GearTier.Add(item);
                   }
               });
                return true;


            }
            


        }

        private DestinyPlayer retrievePlayer(string gamertag)
        {
            //if (ConfigurationManager.AppSettings[gamertag] != null)
            //{
            //    var texto = ConfigurationManager.AppSettings[gamertag];
            //    var p = Newtonsoft.Json.JsonConvert.DeserializeObject<Player>(texto);
            //    return p;
            //}
            //else
                return null;
        }

        private void savePlayer(DestinyPlayer player)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            var texto = Newtonsoft.Json.JsonConvert.SerializeObject(player);
            if (settings[player.GamerTag] != null)
            {
                settings[player.GamerTag].Value = texto;
            }
            else
                settings.Add(player.GamerTag,texto);
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            bool resultado = false;
            
            if (string.IsNullOrWhiteSpace(txt_gt.Text) || cb_platform.SelectedValue == null)
            {
                MessageBox.Show("Gamertag and Platorm are required. ");
            }
            else
            {
                var valor = (cb_publicAPI.IsChecked.HasValue ? cb_publicAPI.IsChecked.Value : true);
                if (!valor)
                {
                    LoginWindow login = new LoginWindow();
                    login.ShowDialog();
                    if (login.Resultado)
                    {
                        var cookiecontainer = login.cookies;

                        resultado = await initPlayerInfo(txt_gt.Text, cb_platform.Text == "Xbox", cookiecontainer);
                    }
                }
                else
                {
                    resultado = await initPlayerInfo(txt_gt.Text, cb_platform.Text == "Xbox");
                }

            }
            ((Button)sender).IsEnabled = true;
            MessageBox.Show(resultado.ToString());
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
                                .Where(t => t.GetType() == typeof(DestinyItemGear))
                                .Cast<DestinyItemGear>()
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