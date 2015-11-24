using Api.Manifest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.tests
{
    [TestClass]
    public class MainApiTests
    {
        private string key = "6def2424db3a4a8db1cef0a2c3a7807e";
        [TestMethod]
        public void MainApi_ManifestNameWorks()
        {
            var api = new Api.API(new OnlineManifest(key));
            var name = api.ManifestTypeName;
            Assert.IsTrue(name == "Api.Manifest.OnlineManifest");

        }
    }
}
