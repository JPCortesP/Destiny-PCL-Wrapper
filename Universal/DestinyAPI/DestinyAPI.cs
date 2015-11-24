﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DestinyAPI.InternalTypes;
using System.Net;

using Windows.Storage;
using System.IO;
using Newtonsoft.Json.Linq;
using Windows.Foundation.Metadata;

namespace DestinyAPI
{
    [Deprecated("Please use the Portable version of this library. Expect breaking API changes", DeprecationType.Remove,(uint)1)]
    public  sealed partial class DestinyAPI : IDestinyAPI
    {
        private string _APIKEY;
        private string filename = "database.json";
        private bool isReady { get { return this.DestinyData != null; } }

        public List<ManifestTable> DestinyData
        { get; set; }
            
        

        /// <summary>
        /// Creates a new instance of DestinyAPI. Use the created object to performs actions. 
        /// Can throw exceptions if you don't specify any parameter, or if you set initManifest to true (for example,
        /// file operations. 
        /// </summary>
        /// <param name="APIKEY">API Key para Bungie.NET</param>
        public DestinyAPI(string APIKEY = "6def2424db3a4a8db1cef0a2c3a7807e")
        {
            this._APIKEY = APIKEY;
            DestinyData = null;
            
        }
        /// <summary>
        /// Loads in memory manifest data. 
        /// </summary>
        /// <returns>bool according to success</returns>
        public async Task<bool> LoadManifestData( bool reloadIfExists = false)
        {
            if (this.DestinyData != null)
            {
                return true;
            }
            if (!await fileExists() || reloadIfExists)
            {
                using (var cliente = new HttpClient())
                {
                    /*
                    ESTA VAINA ES PARA DESCARGAR UNO NUEVO. */
                    //var resultado = await cliente.GetStringAsync("http://destinydb.azurewebsites.net/api/manifest");
                    //var objeto = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ManifestTable>>(resultado);
                    //StorageFile datos = await Windows.Storage.ApplicationData.Current.LocalFolder
                    //    .CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                    //var taskEscribirTexto = Windows.Storage.FileIO.WriteTextAsync(datos, resultado);
                    //Task.WaitAll(taskEscribirTexto.AsTask());
                    //this.DestinyData = objeto;

                    var path = Windows.ApplicationModel.Package.Current.InstalledLocation.Path + @"\DestinyAPI";
                    var folder = await StorageFolder.GetFolderFromPathAsync(path);
                    var file = await folder.GetFileAsync(filename);
                    if (await fileExists())
                    {
                        await file.CopyAndReplaceAsync(await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename));
                    }
                    else
                    {
                        await file.CopyAsync(Windows.Storage.ApplicationData.Current.LocalFolder);
                    }
                    if (await fileExists())
                    {
                        var archivoDatos = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                        var texto = await FileIO.ReadTextAsync(archivoDatos);
                        this.DestinyData = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject<List<ManifestTable>>(texto));
                        return DestinyData != null;
                    }
                    return false;

                    /*****ESTA VAINA ES PARA USAR EL DESPLEGADO. ****/


                }
            }
            else
            {
                if (await fileExists())
                {
                    var archivoDatos = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                    var texto = await FileIO.ReadTextAsync(archivoDatos);
                    this.DestinyData = await Task.Run( () => Newtonsoft.Json.JsonConvert.DeserializeObject<List<ManifestTable>>(texto) );
                    return DestinyData != null;
                }
                return false;
            }
        }

        





        /// <summary>
        /// Returns a Player Object, with a [optional] Character Collection, from
        /// a user object. You can build your own user object. IF you provided the constructor 
        /// with a Cookie collection, then you can send an empty BungieUser object
        /// </summary>
        /// <param name="user">The user to search for</param>
        /// <returns>Player object if found, null otherwise</returns>
        public async Task<Player> GetPlayer(BungieUser user)
        {
            if (!isReady)
            {
                throw new NotImplementedException("No se ha hecho la vaina de hacerlo sin Data");
            }
            var url = String.Format(APIUrls.URls["SearchPlayer"], (int)user.type, user.GamerTag);
            var SearchResultJS = await GetStringAsync(url, user.cookies);
            var SearchResult = Newtonsoft.Json.JsonConvert.DeserializeObject<InternalTypes.PlayerSearchResult>(SearchResultJS);
            //dynamic SearchResult = Newtonsoft.Json.JsonConvert.DeserializeObject(SearchResultJS);
            if (SearchResult.ErrorStatus != "Success")
            {
                return null;
            }
            
            url = string.Format(APIUrls.URls["GetPlayerDetail"], (int)user.type, SearchResult.Response);
            
            var PlayerResultJS = await GetStringAsync(url, user.cookies);
            var PlayerResult = await Task.Run(() =>
                   Newtonsoft.Json.JsonConvert.DeserializeObject<InternalTypes.PlayerResultRootObject>(PlayerResultJS));
            var retorno = await ConvertirAPlayer(PlayerResult, user);
            return retorno;

        }

        private async Task<string> GetStringAsync(string url, CookieContainer cookies = null)
        {
            HttpClient hc;
            if (cookies !=null)
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

            }
            else
                hc = new HttpClient();

            
            
            using (hc)
            {
                hc.DefaultRequestHeaders.Add("X-API-Key", _APIKEY);

                return await hc.GetStringAsync(url);

            }
        }


        #region Helpers
        private async Task<bool> fileExists()
        {
            try
            {
                StorageFile datos = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
        private async Task<Player> ConvertirAPlayer(PlayerResultRootObject playerResult, BungieUser user)
        {
            if (playerResult.ErrorStatus != "Success")
            {
                return null;
            }
            var pr = playerResult.Response.data;
            return await Task.Run(() =>
           {
               var pl = new Player()
               {
                   GamerTag = user.GamerTag,
                   Grimoire = pr.characters.First().characterBase.grimoireScore,
                   MembershipId = pr.characters.First().characterBase.membershipId,

                   type = user.type,
                   Characters = new List<Character>(),
                   Items = new List<ItemBase>()
               };
               foreach (var item in pr.items)
               {
                   var dbData = this.GetItemData(item.itemHash.ToString());
                   var bucketData = this.getBucketData(item.bucketHash);
                   ItemBase newi;
                   if (isGear((string)dbData.itemTypeName))
                   {
                       var statData = this.getStatsData(item.primaryStat.statHash.ToString());
                       
                       newi = new ItemGear(
                           (Int64)item.itemHash, item.itemId, (object)dbData, item.quantity, (object)bucketData,
                       item.isGridComplete, item.transferStatus, item.state, item.characterIndex,
                       item.bucketHash,
                       item.damageType, (Int64)item.damageTypeHash, item.primaryStat.maximumValue, (Int64)item.primaryStat.statHash,
                       item.primaryStat.value, (object)statData
                       );
                       if (newi.dbData != null)
                       {
                           if (newi.dbData.stats != null)
                           {
                               ((ItemGear)newi).BaseStats = new List<StatBase>();
                               foreach (dynamic stat in newi.dbData.stats)
                               {
                                   StatBase Base = new StatBase();
                                   Base.statHash = stat.First.statHash;
                                   Base.value = stat.First.value;
                                   Base.minimum = stat.First.minimum;
                                   Base.maximum = stat.First.maximum;
                                   var resultado = getStatsData(Base.statHash);
                                   Base.statName = resultado.statName;
                                   ((ItemGear)newi).BaseStats.Add(Base);
                               }

                           }
                       }
                       
                   }
                   else
                   {
                       newi = new ItemBase((Int64)item.itemHash, item.itemId, (object)dbData, item.quantity, (object)bucketData,
                      item.isGridComplete, item.transferStatus, item.state, item.characterIndex,
                      item.bucketHash);
                   }



                   //newi.damageType = item.damageType;
                   //newi.damageTypeHash = item.damageTypeHash;

                   //if (item.primaryStat != null)
                   //{
                   //    newi.primaryStats_maximumValue = item.primaryStat.maximumValue;
                   //    newi.primaryStats_statHash = item.primaryStat.statHash.ToString();
                   //    newi.primaryStats_value = item.primaryStat.value;
                   //}
                   //Inject Manifest DATA
                   //if (this.DestinyData != null)
                   //{
                   //    if (item.primaryStat != null)
                   //    {
                   //        newi.statData = this.DestinyData.getStatsData(newi.primaryStats_statHash);
                   //    }

                   //}

                   pl.Items.Add(newi);
               }
               int charCount = 0;
               foreach (var item in pr.characters)
               {
                   var pers = new Character();
                   pers.BaseLevel = item.characterLevel;
                   pers.EmblemBackgroundPath = item.backgroundPath;
                   pers.EmblemHash = item.emblemHash.ToString();
                   pers.EmblemPath = item.emblemPath;
                   pers.LightLevel = item.characterBase.powerLevel;
                   pers.CharacterId = item.characterBase.characterId;

                   pers.Class = getClass(item.characterBase.classType);
                   pers.Gender = getGender(item.characterBase.genderType);
                   pers.Race = getRace(item.characterBase.raceHash);
                   pers.Items = pl.Items.Where(g => g.characterIndex == charCount).ToList();
                   pl.Characters.Add(pers);
                   charCount++;
               }


               return pl;
           }
            );
        }

        private dynamic getStatsData(string v)
        {
            var tabla = DestinyData.Where(t => t.TableName == "DestinyStatDefinition")
                .First();
            var value = tabla.Rows.Where(g => g.id == v).First().Json;
            return JObject.Parse(value);
        }

        private dynamic getBucketData(string bucketHash)
        {
            var tabla = DestinyData.Where(t => t.TableName == "DestinyInventoryBucketDefinition")
                .First();
            var value = tabla.Rows.Where(g => g.id == bucketHash).First().Json;
            return JObject.Parse(value);
        }

        private dynamic GetItemData(string v) //DestinyInventoryItemDefinition
        {
            var tabla = DestinyData.Where(t => t.TableName == "DestinyInventoryItemDefinition")
                .First();
            var value = tabla.Rows.Where(g => g.id == v).First().Json;
            return JObject.Parse(value);
        }

        private string getRace(object v)
        {
            switch (v.ToString())
            {
                case "3887404748":
                    return "Human";
                case "2803282938":
                    return "Awoken";
                case "898834093":
                    return "Exo";
                default:
                    return v.ToString();
            }
        }

        private string getGender(long genderType)
        {
            return genderType == 0 ? "Male" : "Female";
        }

        private string getClass(long classType)
        {
            switch (classType)
            {
                case 0:
                    return "Titan";
                case 1:
                    return "Hunter";
                case 2:
                    return "Warlock";
                default:
                    return classType.ToString();
            }
        }

        private bool isGear (string itemNameType)
        {
            var gearTypes = new List<string>()
            {
                "Scout Rifle"
                ,"Sniper Rifle"
                ,"Sword"
                ,"Ghost Shell"
                ,"Helmet"
                ,"Gauntlets"
                ,"Chest Armor"
                ,"Leg Armor"
                ,"Titan Mark"
                ,"Titan Artifact"
                ,"Pulse Rifle"
                ,"Rocket Launcher"
                ,"Hunter Cloak"
                ,"Hunter Artifact"
                ,"Warlock Bond"
                ,"Warlock Artifact"
                ,"Shotgun"
                ,"Auto Rifle"
            };
            return gearTypes.Contains(itemNameType);
                    
        }
        #endregion

    }

   
}
