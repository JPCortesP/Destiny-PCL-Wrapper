
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
            if (defaultManifest == null)
            {
                throw new NotSupportedException("A MANIFEST, FROM THE PRISON OF MANIFESTS, IS REQUIRED. -Dinklebot");
            }
            
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

        
        public async Task<DestinyClan> GetPlayerClan(DestinyPlayer player)
        {
            
            //First: https://bungie.net/Platform/User/GetBungieAccount/{membId}/{MembType}/
            //with the GroupdID: https://bungie.net/Platform/Group/{GroupID}/MembersV3?currentPage=1&itemsPerPage=50
            var url = String.Format("https://bungie.net/Platform/User/GetBungieAccount/{0}/{1}", player.MembershipId, (int)player.type);
            var getbungieaccount = await getString(url, player.cookies);
            dynamic bungieaccount = JObject.Parse(getbungieaccount);
            var groupId = (int)((dynamic)((dynamic)bungieaccount).Response).clans[0].groupId;
            var ClanInfo = ((dynamic)((dynamic)bungieaccount).Response).relatedGroups[groupId.ToString()];
            url = string.Format("https://bungie.net/Platform/Group/{0}/MembersV3?currentPage=1&itemsPerPage=50", groupId);
            var players_string = await getString(url, player.cookies);
            var playersInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<InternalTypes.ClanPlayersResults_RootObject>(players_string);
            return new DestinyClan(groupId, (object)ClanInfo, playersInfo.Response.results);

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
                p.cookies = user.cookies;
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
        public IEnumerable<DestinyPlayer> getPlayersLoop (List<BungieUser> users)
        {
            var userscopy = users.Distinct(new bungieuserComparador()).ToList();
            foreach (var user in userscopy)
            {
                user.cookies = null;
                yield return this.getPlayerAsync(user).Result;
            }
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

    internal class bungieuserComparador : EqualityComparer<BungieUser>
    {
        public override bool Equals(BungieUser x, BungieUser y)
        {
            return x?.GamerTag.ToLower() == y?.GamerTag.ToLower() &&
                x?.type == y?.type;
        }

        public override int GetHashCode(BungieUser obj)
        {
            return 1;
        }
    }
}
