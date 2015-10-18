using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            object id = this.Session["connected"];
            if (id != null)
            {
                ViewBag.isAdmin = Library.Models.User.isUserAdmin(id.ToString());
                //ViewBag.isAdmin = Library.Models.User.isUserAdmin(Convert.ToInt32(id));
            }
            else
            {
                ViewBag.isAdmin = false;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}