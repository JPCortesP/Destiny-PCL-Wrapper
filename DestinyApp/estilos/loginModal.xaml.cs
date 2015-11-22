using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http.Filters;

namespace DestinyApp
{
    public partial class loginModal : ContentDialog
    {
        internal CookieContainer cookies { get; set; }

        public loginModal( DestinyAPI.MembershipType type)
        {
            InitializeComponent();
            
            wb_login.NavigationCompleted += Wb_login_NavigationCompleted;
            var url = "";
            switch (type)
            {
                case DestinyAPI.MembershipType.Xbox:
                    url = "https://www.bungie.net/en/User/SignIn/Xuid?bru=%252f";
                    break;
                case DestinyAPI.MembershipType.PSN:
                    url = "https://www.bungie.net/en/User/SignIn/Psnid?bru=%252f";
                    break;
                default:
                    break;
            }
            wb_login.NavigateWithHttpRequestMessage(new Windows.Web.Http.HttpRequestMessage()
            {
                RequestUri = new Uri(url)
                
            });
        }

        private void Wb_login_NavigationCompleted(Windows.UI.Xaml.Controls.WebView sender, Windows.UI.Xaml.Controls.WebViewNavigationCompletedEventArgs args)
        {
            HttpBaseProtocolFilter baseFilter = new HttpBaseProtocolFilter();
            var cookies = baseFilter.CookieManager.GetCookies(sender.Source);
            var cookie_bungledid = cookies.Where(g => g.Name == "bungledid").Count() > 0;
            var cookie_bungleme = cookies.Where(g => g.Name == "bungleme").Count() > 0;
            var cookie_bungleloc = cookies.Where(g => g.Name == "bungleloc").Count() > 0;
            var cookie_bungled = cookies.Where(g => g.Name == "bungled").Count() > 0;
            if (cookie_bungled && cookie_bungledid && cookie_bungleloc && cookie_bungleme)
            {
                //var Cookiesimportantes = cookies.Where(t => 
                //t.Name == "bungledid"
                //|| t.Name == "bungleme"
                //|| t.Name == "bungleloc"
                //|| t.Name == "bungled"
                // ).ToList();
                var cookieContainer = new CookieContainer();
                foreach (var item in cookies)
                {
                    cookieContainer.Add(sender.Source, new Cookie()
                    {
                        Domain = item.Domain,
                        //Expires =  new DateTime(item.Expires.Value.Ticks),
                        HttpOnly = item.HttpOnly,
                        Name =  item.Name,
                        Path = item.Path,
                        Value = item.Value
                        
                    });
                }
                this.cookies = cookieContainer;
                this.wb_login.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.confirmation.Visibility = Windows.UI.Xaml.Visibility.Visible;
                

            }
            
        }
    }
}
