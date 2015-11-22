using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            cbb_platform.DataContext = new List<DestinyAPI.MembershipType>() { DestinyAPI.MembershipType.Xbox, DestinyAPI.MembershipType.PSN };
            cbb_platform.SelectedIndex = 0;


        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            var mostrar = settings.Values["alpha_dialog_showed"];
            base.OnNavigatedTo(e);
            if (mostrar == null)
            {
                ContentDialog Show_Alpha_Warning = new ContentDialog()
                {
                    Title = "THIS IS AN ALPHA VERSION",
                    Content = "THANK YOU FOR HELPING ME TEST THE APPLICATION\n" +
                "Please remember that this is an alpha version, so it might not work as expected. "
                + "This, however, is why you're testing this. If you have ANY kind of feedback, send it"
                + " to me and I'll be eternally grateful, even more if you carried me to that place in "
                + "Mercury I'll never be.\nAlso, the bungie API is not destructive, so a error in this"
                + " App won't dismantle your precious NotOPAnymoreHorn.\n"
                + "Again, THANK YOU\n"
                + "This message should not show again until next update. Again, SHOULD"
                ,
                    PrimaryButtonText = "Ok"
                };

                await Show_Alpha_Warning.ShowAsync();
                settings.Values["alpha_dialog_showed"] = true;
            }

        }

        private async void Load_Withouth_Auth_Click(object sender, RoutedEventArgs e)
        {
            await Load(sender);
        }
        private async void LoginLoad_Click(object sender, RoutedEventArgs e)
        {
            await Load(sender, true);
        }

        private async System.Threading.Tasks.Task Load(object sender, bool auth = false)
        {
            ((Button)sender).IsEnabled = false;

            if (!string.IsNullOrWhiteSpace(txt_gt.Text))
            {
                CookieContainer cookies = null;
                if (auth)
                {
                    var lm = new loginModal((DestinyAPI.MembershipType)cbb_platform.SelectedValue);

                    var res = await lm.ShowAsync();
                    if (res == ContentDialogResult.Primary)
                    {
                        cookies = lm.cookies;
                    }

                }
                dataCargada = await api.LoadManifestData();

                player = await api.GetPlayer(new DestinyAPI.BungieUser()
                {
                    GamerTag = txt_gt.Text,
                    type = (DestinyAPI.MembershipType)cbb_platform.SelectedValue,
                    cookies = cookies
                });
                if (player != null)
                {
                    var itemsType = player.Gear.Select(g => g.itemTypeName).Distinct().ToList();
                    itemsType.Insert(0, "---- All ----");
                    cbb_GearType.DataContext = itemsType;

                    sp_filter.Visibility = Visibility.Visible;
                    cbb_GearType.SelectedIndex = 0;
                }
                else
                {
                    ContentDialog errorLoading = new ContentDialog()
                    {
                        Title = "Error Loading Player with GT: " + txt_gt.Text,
                        Content = "No player with GT: " + txt_gt.Text + " found on Bungie. Please " +
                        "check the values (GT and Platform) and try again.\nRemember that Gamertag" +
                        " is case sensitive.",
                        PrimaryButtonText = "Ok"
                    };
                    await errorLoading.ShowAsync();
                }

            }
            ((Button)sender).IsEnabled = true;
        }

        private void cbb_GearType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbb_GearType.SelectedValue != null)
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
}
