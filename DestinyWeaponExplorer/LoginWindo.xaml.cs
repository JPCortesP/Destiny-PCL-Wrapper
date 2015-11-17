﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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

namespace DestinyWeaponExplorer
{
    /// <summary>
    /// Interaction logic for LoginWindo.xaml
    /// </summary>
    public partial class LoginWindo : Window
    {
        public CookieContainer cookies;
        private Uri url = new Uri("https://www.bungie.net");
        public bool Resultado = false;
        public LoginWindo()
        {
            InitializeComponent();
            browser.Navigate("https://www.bungie.net/en/User/SignIn/Xuid?bru=%252f");
            browser.Navigated += Browser_Navigated;
        }

        private void Browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            cookies = GetUriCookieContainer(url);
            var temp = cookies.GetCookies(url);
            if (temp.Count > 3)
            {
                this.Resultado = true;
                this.Close();
                
            }
                
            
        }

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookieEx(
        string url,
        string cookieName,
        StringBuilder cookieData,
        ref int size,
        Int32 dwFlags,
        IntPtr lpReserved);

        private const Int32 InternetCookieHttponly = 0x2000;

        /// <summary>
        /// Gets the URI cookie container.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static CookieContainer GetUriCookieContainer(Uri uri)
        {
            CookieContainer cookies = null;
            // Determine the size of the cookie
            int datasize = 8192 * 16;
            StringBuilder cookieData = new StringBuilder(datasize);
            if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, InternetCookieHttponly, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;
                // Allocate stringbuilder large enough to hold the cookie
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookieEx(
                    uri.ToString(),
                    null, cookieData,
                    ref datasize,
                    InternetCookieHttponly,
                    IntPtr.Zero))
                    return null;
            }
            if (cookieData.Length > 0)
            {
                cookies = new CookieContainer();
                cookies.SetCookies(uri, cookieData.ToString().Replace(';', ','));
            }
            return cookies;
        }
    }
}