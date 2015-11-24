using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api;
using Api.Manifest;
using Microsoft.CSharp;

namespace API.tests
{
    [TestClass]
    public class ManifestTests
    {
        private string key = "6def2424db3a4a8db1cef0a2c3a7807e";
        [TestMethod]
        public void Manifest_ReturnsItemData()
        {
            using (var manifest = new OnlineManifest(key))
            {
                dynamic respuesta = manifest.getData(ManifestTable.InventoryItem, "1256644900").Result;
                Assert.IsNotNull(respuesta);
                 //.data.inventoryItem.itemName;
                dynamic itemName = respuesta.itemName;
                Assert.IsNotNull(itemName);
            }
        }

        [TestMethod]
        public void Manifest_ReturnsActivityData()
        {
            using (var manifest = new OnlineManifest(key))
            {
                dynamic respuesta = manifest.getData(ManifestTable.Activity, "1005705920").Result;
                Assert.IsNotNull(respuesta);
                //.data.inventoryItem.itemName;
                dynamic itemName = respuesta.activityName;
                Assert.IsNotNull(itemName);
            }
        }

        [TestMethod]
        public void Manifest_ReturnsGenderData()
        {
            using (var manifest = new OnlineManifest(key))
            {
                dynamic respuesta = manifest.getData(ManifestTable.Gender, "3111576190").Result;
                Assert.IsNotNull(respuesta);
                Assert.IsTrue(((string)respuesta) == "Male");
                
            }
        }

        

        [TestMethod]
        public void Manifest_ReturnsInventoryBucketData()
        {
            using (var manifest = new OnlineManifest(key))
            {
                dynamic respuesta = manifest.getData(ManifestTable.InventoryBucket, "138197802").Result;
                Assert.IsNotNull(respuesta);
                //.data.inventoryItem.itemName;
                dynamic itemName = respuesta.bucketIdentifier;
                Assert.IsNotNull(itemName);
                var vaultText = (string)respuesta.bucketIdentifier;
                Assert.IsTrue(vaultText == "BUCKET_VAULT_ITEMS");
            }
        }

       

        [TestMethod]
        public void Manifest_ReturnsRaceData()
        {
            using (var manifest = new OnlineManifest(key))
            {
                dynamic respuesta = manifest.getData(ManifestTable.Race, "3887404748").Result;
                Assert.IsNotNull(respuesta);
                var shouldbeHuman = (string)respuesta;
                Assert.IsTrue(shouldbeHuman == "Human");
               
            }
        }

        [TestMethod]
        public void Manifest_ReturnsStatData()
        {
            using (var manifest = new OnlineManifest(key))
            {
                dynamic respuesta = manifest.getData(ManifestTable.Stat, "2996146975").Result;
                Assert.IsNotNull(respuesta);
                //.data.inventoryItem.itemName;
                dynamic itemName = respuesta.statName;
                Assert.IsNotNull(itemName);
                string name = (string)respuesta.statName;
                Assert.IsTrue(name == "Agility");
            }
        }

        [TestMethod]
        public void Manifest_ReturnsClassData()
        {
            using (var manifest = new OnlineManifest(key))
            {
                dynamic respuesta = manifest.getData(ManifestTable.Class, "3655393761").Result;
                Assert.IsNotNull(respuesta);
                var shouldbeTitan = (string)respuesta;
                Assert.IsTrue(shouldbeTitan == "Titan");
                
            }
        }
    }
}
