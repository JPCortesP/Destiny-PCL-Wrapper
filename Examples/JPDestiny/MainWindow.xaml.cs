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

namespace JPDestiny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            if (string.IsNullOrWhiteSpace(txt_gt.Text))
            {
                ((Button)sender).IsEnabled = true;
                MessageBox.Show("The gamertag/PSN ID is required. Also, make sure your Platform selected is correct", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                var isXbox = cb_platform.SelectedIndex == 0;
                var user = new DestinyPCL.Objects.BungieUser(txt_gt.Text, isXbox ? DestinyPCL.Objects.DestinyMembershipType.Xbox : DestinyPCL.Objects.DestinyMembershipType.PSN);
                if (chkb_login.IsChecked.Value)
                {
                    throw new NotImplementedException("Not yet deary. I have not implemented the private login feature, YET. But I'll work faster");
                }
                App.Player = await App.API.getPlayerAsync(user);
                if (App.Player == null)
                {
                    MessageBox.Show("Can't find the GT/PSN and Platform combination", "Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    this.Hide();
                    PlayerDetail pd = new PlayerDetail();
                    pd.Closed += (sendeyr, args) =>
                    {
                        this.Close();
                    };
                    pd.Show();
                    
                }
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show("An Error in the request has occured. please check that you have a working internet connection. and try again" +
                        "", "Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A weird error has just happened. Sorry, even Dinklebot made mistakes (remember Sepiks Prime).\n Now, here is the full information so you" +
                    " can report this to the forums: \n" +
                       ex.ToString(), "Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            finally
            {
                ((Button)sender).IsEnabled = true;
            }
            
        }
    }
}
