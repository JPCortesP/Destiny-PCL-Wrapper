﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api;
using Api.Manifest;
using Microsoft.CSharp;


namespace API.tests
{
    [TestClass]
    public class ManifestTests
    {
        private static string key = "6def2424db3a4a8db1cef0a2c3a7807e";
        private DestinyManifest Getmanifest { get { return new OfflineManifest(key); } }
        [TestMethod]
        public void Manifest_ReturnsItemData()
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
        public void Manifest_ReturnsActivityData()
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
        public void Manifest_ReturnsGenderData()
        {
            using (var manifest = Getmanifest)
            {
                dynamic respuesta = manifest.getData(ManifestTable.Gender, "3111576190");
                Assert.IsNotNull(respuesta);
                Assert.IsTrue(((string)respuesta.genderName) == "Male");
                
            }
        }

        

        [TestMethod]
        public void Manifest_ReturnsInventoryBucketData()
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
        public void Manifest_ReturnsRaceData()
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
        public void Manifest_ReturnsStatData()
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
        public void Manifest_ReturnsClassData()
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