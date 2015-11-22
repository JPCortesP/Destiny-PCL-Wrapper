using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DestinyApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DestinyAPI.DestinyAPI api = new DestinyAPI.DestinyAPI();
        DestinyAPI.Player player;
        bool dataCargada = false;
        public MainPage()
        {
            this.InitializeComponent();
            //status.Text = "Componentes Inicializados";
            cbb_platform.DataContext = new List<DestinyAPI.MembershipType> () { DestinyAPI.MembershipType.Xbox, DestinyAPI.MembershipType.PSN };
            cbb_platform.SelectedIndex = 0;

            
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //    var api = new DestinyAPI.DestinyAPI();
            //    //status.Text = "API Inicializado";
            //    var resultado =await api.LoadManifestData(false);
            //    //status.Text = "Manifesto Cargado";
            //    var player = await api.GetPlayer(new DestinyAPI.BungieUser() { GamerTag = "JPCortesP",
            //    type = DestinyAPI.MembershipType.Xbox});

            //    //this.lv_chars.DataContext = player.Gear;
            //
            dataCargada = await api.LoadManifestData();
            btn_load.IsEnabled = dataCargada;
            if (!dataCargada)
            {
                
            }
            else
            {

            }

        }

        private async void Load_Withouth_Auth_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            if (!string.IsNullOrWhiteSpace(txt_gt.Text))
            {
                player = await api.GetPlayer(new DestinyAPI.BungieUser()
                {
                    GamerTag = txt_gt.Text,
                    type = (DestinyAPI.MembershipType)cbb_platform.SelectedValue
                });
                this.lv_gear.DataContext = player.Gear;
                var itemsType = player.Gear.Select(g => g.itemTypeName).Distinct().ToList();
                itemsType.Insert(0, "---- All ----");
                cbb_GearType.DataContext = itemsType;
                ((Button)sender).IsEnabled = true;
                sp_filter.Visibility = Visibility.Visible;
            }
        }

        private void cbb_GearType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (player != null)
            {
                if (cbb_GearType.SelectedValue.ToString() == "---- All ----")
                {
                    this.lv_gear.DataContext = player.Gear;
                }
                else
                {
                    var gear = player.Gear.Where(g => g.itemTypeName == cbb_GearType.SelectedValue.ToString()).ToList();
                    this.lv_gear.DataContext = gear;
                }
            }

        }
    }
}
