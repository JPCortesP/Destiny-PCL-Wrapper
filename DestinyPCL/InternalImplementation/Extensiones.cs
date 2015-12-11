
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
    public partial class DestinyService : IDestinyService
    {
        private async Task<string> getString(string url, CookieContainer cookies = null)
        {
            if (cookies != null)
            {
                hc = new HttpClient(new HttpClientHandler() { CookieContainer = cookies });
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
