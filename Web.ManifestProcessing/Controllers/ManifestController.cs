using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.ManifestProcessing.Controllers
{
    public class ManifestController : ApiController
    {
        public List<Models.ManifestTable> Get()
        {
            var algo = Models.Manifest.Create();
            return algo.Tables;
        }
    }
}
