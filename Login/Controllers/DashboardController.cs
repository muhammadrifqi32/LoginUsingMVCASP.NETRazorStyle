using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["id"] != null)
            {
            return View();
            }
            else
            {
                return RedirectToAction("Index", "Users");
            }
        }
    }
}