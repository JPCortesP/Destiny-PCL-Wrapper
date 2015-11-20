using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DestinyAPI
{
    public sealed partial class DestinyAPI : IDestinyAPI
    {
        private async Task<bool> downloadManifest(bool reloadIfExists)
        {
            Windows.Storage.StorageFolder Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            if (!reloadIfExists)
            {
                try
                {
                    var archivo = await Folder.GetFileAsync("data.content");
                    return true;
                }   
                catch (Exception)
                {

                }
            }
            
            using (var hc = new HttpClient())
            {
                //Downloads and extract the fucking file
                try
                {

                    var tm = await Folder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);
                    foreach (var item in tm)
                    {
                        await item.DeleteAsync();
                        
                    }

                    hc.DefaultRequestHeaders.Add("X-API-Key", "6def2424db3a4a8db1cef0a2c3a7807e");
                    var initialAnswer = hc.GetStringAsync("http://www.bungie.net/platform/destiny/manifest/").Result;
                    dynamic jsonInitialAnswer = JObject.Parse(initialAnswer);
                    var path = (string)jsonInitialAnswer.Response.mobileWorldContentPaths.en.Value;
                    Uri url = new Uri("http://bungie.net/" + path);
                    var fileStream = hc.GetStreamAsync(url).Result;

                    //Se guarda el archivo como ZIP. 

                    var TempZipFile = await Folder.CreateFileAsync("Temp.zip", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    var StreamParaGuardarZip = await TempZipFile.OpenStreamForWriteAsync();
                    fileStream.CopyTo(StreamParaGuardarZip);
                    StreamParaGuardarZip.Dispose();
                    //Archivo ZIP Guardado. 

                    //Ahora a Descomprimirlo
                    ZipFile.ExtractToDirectory(TempZipFile.Path, Folder.Path);

                    //Cambiarle de Nombre
                    var archivoDatos = await Folder.GetFileAsync(Path.GetFileName(url.AbsolutePath));
                    var renameTask = archivoDatos.RenameAsync("data.content");
                    var DeleteTask = TempZipFile.DeleteAsync();

                    Task.WaitAll(renameTask.AsTask(), DeleteTask.AsTask());
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                    //throw ex;
                }
            }
        }
        private Task<bool> CargarManifestData()
        {
            throw new NotImplementedException();
        }
    }
}
