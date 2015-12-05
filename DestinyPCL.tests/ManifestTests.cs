using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DestinyPCL;
using DestinyPCL.Manifest;
using Microsoft.CSharp;
using DestinyPCL.Win32Manifest;

namespace API.tests
{
    [TestClass]
    public class Win32ManifestTests
    {
        private static string key = "6def2424db3a4a8db1cef0a2c3a7807e";
        private DestinyManifest Getmanifest { get { return new Win32Manifest(key); } }
        [TestMethod]
        public void OfflineManifest_ReturnsItemData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.InventoryItem, "1703777169");
                Assert.IsNotNull(respuesta);
                 //.data.inventoryItem.itemName;
                string itemName = respuesta.itemName;
                Assert.AreEqual(itemName, "1000-Yard Stare");
                Assert.IsNotNull(itemName);
            }
        }

        [TestMethod]
        public void OfflineManifest_ReturnsActivityData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.Activity, "1005705920");
                Assert.IsNotNull(respuesta);
                //.data.inventoryItem.itemName;
                dynamic itemName = respuesta.activityName;
                Assert.IsNotNull(itemName);
            }
        }

        [TestMethod]
        public void OfflineManifest_ReturnsGenderData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.Gender, "3111576190");
                Assert.IsNotNull(respuesta);
                Assert.IsTrue(((string)respuesta.genderName) == "Male");
                
            }
        }

        

        [TestMethod]
        public void OfflineManifest_ReturnsInventoryBucketData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.InventoryBucket, "138197802");
                Assert.IsNotNull(respuesta);
                //.data.inventoryItem.itemName;
                dynamic itemName = respuesta.bucketIdentifier;
                Assert.IsNotNull(itemName);
                var vaultText = (string)respuesta.bucketIdentifier;
                Assert.IsTrue(vaultText == "BUCKET_VAULT_ITEMS");
            }
        }

       

        [TestMethod]
        public void OfflineManifest_ReturnsRaceData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.Race, "3887404748");
                Assert.IsNotNull(respuesta);
                var shouldbeHuman = (string)respuesta.raceName;
                Assert.IsTrue(shouldbeHuman == "Human");
               
            }
        }

        [TestMethod]
        public void OfflineManifest_ReturnsStatData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.Stat, "368428387");
                Assert.IsNotNull(respuesta);
                //.data.inventoryItem.itemName;
                dynamic itemName = respuesta.statName;
                Assert.IsNotNull(itemName);
                string name = (string)respuesta.statName;
                Assert.IsTrue(name == "Attack");
           } 
        }

        [TestMethod]
        public void OfflineManifest_ReturnsClassData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.Class, "3655393761");
                Assert.IsNotNull(respuesta);
                var shouldbeTitan = (string)respuesta.className;
                Assert.IsTrue(shouldbeTitan == "Titan");
                
            }
        }
    }

    [TestClass]
    public class OnlineManifestTests
    {
        private static string key = "6def2424db3a4a8db1cef0a2c3a7807e";
        private DestinyManifest Getmanifest { get { return new OnlineManifest(key); } }
        [TestMethod]
        public void OnlineManifest_ReturnsItemData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.InventoryItem, "1256644900");
                Assert.IsNotNull(respuesta);
                //.data.inventoryItem.itemName;
                dynamic itemName = respuesta.itemName;
                Assert.IsNotNull(itemName);
            }
        }

        [TestMethod]
        public void OnlineManifest_ReturnsActivityData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.Activity, "1005705920");
                Assert.IsNotNull(respuesta);
                //.data.inventoryItem.itemName;
                dynamic itemName = respuesta.activityName;
                Assert.IsNotNull(itemName);
            }
        }

        [TestMethod]
        public void OnlineManifest_ReturnsGenderData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.Gender, "3111576190");
                Assert.IsNotNull(respuesta);
                Assert.IsTrue(((string)respuesta.genderName) == "Male");

                //dynamic f = 

            }
        }



        [TestMethod]
        public void OnlineManifest_ReturnsInventoryBucketData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.InventoryBucket, "138197802");
                Assert.IsNotNull(respuesta);
                //.data.inventoryItem.itemName;
                dynamic itemName = respuesta.bucketIdentifier;
                Assert.IsNotNull(itemName);
                var vaultText = (string)respuesta.bucketIdentifier;
                Assert.IsTrue(vaultText == "BUCKET_VAULT_ITEMS");
            }
        }



        [TestMethod]
        public void OnlineManifest_ReturnsRaceData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.Race, "3887404748");
                Assert.IsNotNull(respuesta);
                var shouldbeHuman = (string)respuesta.raceName;
                Assert.IsTrue(shouldbeHuman == "Human");

            }
        }

        [TestMethod]
        public void OnlineManifest_ReturnsStatData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.Stat, "2996146975");
                Assert.IsNotNull(respuesta);
                //.data.inventoryItem.itemName;
                dynamic itemName = respuesta.statName;
                Assert.IsNotNull(itemName);
                string name = (string)respuesta.statName;
                Assert.IsTrue(name == "Agility");
            }
        }

        [TestMethod]
        public void OnlineManifest_ReturnsClassData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.Class, "3655393761");
                Assert.IsNotNull(respuesta);
                var shouldbeTitan = (string)respuesta.className;
                Assert.IsTrue(shouldbeTitan == "Titan");

            }
        }
    }
}
