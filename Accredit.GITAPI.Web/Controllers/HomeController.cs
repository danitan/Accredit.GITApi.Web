using Accredit.GITAPI.Service;
using Accredit.GITAPI.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Accredit.GITAPI.Web.Controllers
{
    public class HomeController : Controller
    {
        private IGITAPIService _GITAPIService;
        public HomeController(IGITAPIService iGITAPIService)
        {
            _GITAPIService = iGITAPIService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Details(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var user = TempData["userResult_" + username] as User;
                if (user != null)
                {
                    ViewBag.Message = JsonConvert.SerializeObject(user);
                    return View(user);
                }
            }

            return RedirectToAction("Search");
        }

        [HttpPost]
        public async Task<ActionResult> Search(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _GITAPIService.GetUser(user.Username);
                    var userResult = JsonConvert.DeserializeObject<User>(result);
                    var tempDataKey = "userResult_" + user.Username;
                    TempData[tempDataKey] = userResult;
                    TempData.Keep(tempDataKey);
                    return RedirectToAction("Details", new { username = user.Username });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"[Error]{ex.Message}";
                }
            }
            return View();
        }
    }
}