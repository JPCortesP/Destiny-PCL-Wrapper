using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DestinyWeb.Controllers
{
    public class DestinyPlayerController : Controller
    {
        public ActionResult NewDP ()
        {
            var user = new DestinyPCL.Objects.BungieUser();
            user.type = DestinyPCL.Objects.DestinyMembershipType.Xbox;
            return View(user);
        }
        [HttpPost]
        public ActionResult NewDP(DestinyPCL.Objects.BungieUser user)
        {
            user.type = DestinyPCL.Objects.DestinyMembershipType.Xbox;
            return RedirectToAction("Index", new { id = user.GamerTag, type = user.type });
        }
        // GET: DestinyPlayer
        public async Task<ActionResult> Index( string id, DestinyPCL.Objects.DestinyMembershipType type)
        {
            using (var api = Models.DestinyAPI.getApiClient())
            {
                var player = await api.getPlayerAsync(new DestinyPCL.Objects.BungieUser(id, type));
                var chars = player.Characters;
                var useles = player.Gear.Count;
                var useless1 = player.Items.Count;
                return View(player);
                
            }
            
            
        }
    }
}