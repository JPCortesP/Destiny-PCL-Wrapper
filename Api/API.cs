
using DestinyPCL.Objects;
using Newtonsoft.Json.Linq;
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
        public DestinyService( DestinyManifest defaultManifest, string _ApiKey)
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
        public Task<bool> EquipItem(DestinyCharacter target, DestinyItemBase item)
        {
            throw new NotImplementedException();
        }

        public Task<DestinyClan> getAllPlayersInClan(BungieUser user)
        {
            throw new NotImplementedException();
        }

        public Task<List<object>> getHistory(BungieUser user)
        {
            throw new NotImplementedException();
        }

        public Task<DestinyItemBase> getInventory(BungieUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<DestinyPlayer> getPlayerAsync(BungieUser user)
        {//http://bungie.net/platform/destiny/SearchDestinyPlayer/1/jpcortesp
            var url = String.Format("http://bungie.net/platform/destiny/SearchDestinyPlayer/{0}/{1}", (int)user.type, user.GamerTag);
            var respuesta = await getString(url,user.cookies);
            dynamic objRespuesta = JObject.Parse(respuesta);
            var algoParaEspacio = objRespuesta;
            dynamic listaRespuesta = ((Newtonsoft.Json.Linq.JContainer)((((dynamic)((dynamic)objRespuesta).Response)))).HasValues;
            if (listaRespuesta)
            {
                dynamic response = ((dynamic)((dynamic)objRespuesta).Response)[0];
                
                url = String.Format("http://www.bungie.net/platform/destiny/{0}/Account/{1}/Items/", (int)user.type, response.membershipId);
                respuesta = await getString(url, user.cookies);
                var _PlayerResult = await Task.Run(() =>
                   Newtonsoft.Json.JsonConvert.DeserializeObject<InternalTypes.PlayerResultRootObject>(respuesta));
                DestinyPlayer p = new DestinyPlayer(Manifest,_PlayerResult.Response.data, user);
                p.GamerTag = response.displayName;
                p.MembershipId = response.membershipId;
                                //var res = convertirAPlayer(_PlayerResult, p);
                if (p != null)
                {
                    return p;
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
        }

        

        public Task<List<DestinyPlayer>> getPlayersAsync(List<BungieUser> users)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoadManifest(DestinyManifest instance)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TransferItem(DestinyCharacter target, DestinyItemBase item, bool toVault = false)
        {
            throw new NotImplementedException();
        }

        
    }
}
