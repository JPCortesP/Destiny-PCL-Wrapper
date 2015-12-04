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
using System.Windows.Shapes;

namespace JPDestiny
{
    /// <summary>
    /// Interaction logic for PlayerDetail.xaml
    /// </summary>
    public partial class PlayerDetail : Window
    {
        DestinyPCL.Objects.DestinyClan Clan;
        List<StackPanel> contenido = new List<StackPanel>();
        public PlayerDetail()
        {
            InitializeComponent();
            contenido.Add(SP_Chars);
        }
        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (App.Player == null)
                throw new NotSupportedException("Oh deary, something wen't very wrong");
            DataContext = App.Player;
            await LoadClanData();
            await LoadLastActivities();

        }

        private Task LoadLastActivities()
        {
            return Task.CompletedTask;
        }

        private async Task<bool> LoadClanData()
        {
            this.Clan = await App.API.GetPlayerClan(App.Player);
            this.StackP_ClanDetailsHeader.DataContext = Clan;
            return true;
        }

        private async void ShowCharacters_Clicked(object sender, RoutedEventArgs e)
        {
            hideEverything();
            SP_Chars.Visibility = Visibility.Visible;
            SP_Chars_txt_Titulo.Text = "Loading Characters";
            var count = await Task.Run( () => App.Player.Characters.Count() );
            SP_Chars_txt_Titulo.Text = "Characters";
            SP_Chars_Listview.DataContext = App.Player.Characters;
        }

        private void hideEverything()
        {
            contenido.ForEach(e => e.Visibility = Visibility.Collapsed );
        }
    }
}
