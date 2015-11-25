using Api.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public partial class API : IApi
    {
        public API( DestinyManifest defaultManifest, string _ApiKey)
        {
            this.Manifest = defaultManifest;
            this.Manifest.ApiKey = _ApiKey;
            this.ApiKey = _ApiKey;
            
        }
        public string ApiKey { get; set; }
        public DestinyManifest Manifest { get; set; }
        private HttpClient hc;

        public string ManifestTypeName
        {
            get
            {
                if (Manifest != null)
                {
                    return Manifest.GetType().ToString();
                }
                else
                    return "NO Manifest in use";
                
            }
        }

        public void Dispose()
        {
            this.ApiKey = "";
            if(Manifest != null)
                this.Manifest.Dispose();
            if (hc != null)
                hc.Dispose();

        }
        public Task<bool> EquipItem(Character target, ItemBase item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Player>> getAllPlayersInClan(BungieUser user)
        {
            throw new NotImplementedException();
        }

        public Task<List<object>> getHistory(BungieUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ItemBase> getInventory(BungieUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<Player> getPlayerAsync(BungieUser user)
        {//http://bungie.net/platform/destiny/SearchDestinyPlayer/1/jpcortesp
            var url = String.Format("http://bungie.net/platform/destiny/SearchDestinyPlayer/{0}/{1}", (int)user.type, user.GamerTag);
            var respuesta = await getString(url,user.cookies);
            dynamic objRespuesta = JObject.Parse(respuesta);
            var algoParaEspacio = objRespuesta;
            dynamic listaRespuesta = ((Newtonsoft.Json.Linq.JContainer)((((dynamic)((dynamic)objRespuesta).Response)))).HasValues;
            if (listaRespuesta)
            {
                dynamic response = ((dynamic)((dynamic)objRespuesta).Response)[0];
                Player p = new Player();
                p.GamerTag = response.displayName;
                p.MembershipId = response.membershipId;
                url = String.Format("http://www.bungie.net/platform/destiny/{0}/Account/{1}/Items/", (int)user.type, p.MembershipId);
                respuesta = await getString(url, user.cookies);
                var _PlayerResult = await Task.Run(() =>
                   Newtonsoft.Json.JsonConvert.DeserializeObject<InternalTypes.PlayerResultRootObject>(respuesta));
                var res = convertirALazyPlayer(_PlayerResult, p);
                if (res != null)
                {
                    return res;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
            throw new NotImplementedException();
        }

        

        public Task<List<Player>> getPlayersAsync(List<BungieUser> users)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoadManifest(DestinyManifest instance)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TransferItem(Character target, ItemBase item, bool toVault = false)
        {
            throw new NotImplementedException();
        }

        
    }
}
