using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyPCL;
using System.Threading.Tasks;

namespace DestinyWeb.Controllers
{
    public class DestinyController : Controller
    {
        DestinyService ds = new DestinyService(new DestinyPCL.Manifest.OnlineManifest(), "6def2424db3a4a8db1cef0a2c3a7807e");
        // GET: Destiny
        public ActionResult Index()
        {
            return RedirectToAction("Player", new { controller = "Destiny", id = "JPCortesP", type = 1 });
        }
        public async Task<ActionResult> Player( string id, int type)
        {
            var Player = await ds.getPlayerAsync(new DestinyPCL.Objects.BungieUser(id, type == 1 ? DestinyPCL.Objects.DestinyMembershipType.Xbox : DestinyPCL.Objects.DestinyMembershipType.PSN));
            return View(Player);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ds.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}