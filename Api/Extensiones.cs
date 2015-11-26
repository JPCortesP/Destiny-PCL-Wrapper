
using DestinyPCL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL
{
    public partial class DestinyService : IDestnyService
    {
        private async Task<string> getString(string url, CookieContainer cookies = null)
        {
            if (cookies != null)
            {
                hc = new HttpClient(new HttpClientHandler() { CookieContainer = cookies });
                var galletas = cookies.GetCookies(new Uri("https://bungie.net"));
                foreach (Cookie item in galletas)
                {
                    if (item.Name == "bungled")
                    {
                        hc.DefaultRequestHeaders.Add("X-csrf", item.Value);
                    }
                }
                galletas = cookies.GetCookies(new Uri("https://www.bungie.net"));
                foreach (Cookie item in galletas)
                {
                    if (item.Name == "bungled")
                    {
                        hc.DefaultRequestHeaders.Add("X-csrf", item.Value);
                    }
                }
            }
            else
            {
                hc = new HttpClient();
            }
            using (hc)
            {
                hc.DefaultRequestHeaders.Add("X-API-Key", this.ApiKey);
                return await hc.GetStringAsync(url);
            }

        }
        

        
    }
}
