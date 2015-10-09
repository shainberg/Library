using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View("LoginNew");
        }

        public ActionResult Login()
        {
            string id = this.Request["id"];
            string password = this.Request.Params["password"];

            if (id != null && password != null)
            {
                /*Query From users table*/
                HttpContext.Session.Add("Connected", id);
            }

            return RedirectToAction("index", "Home");

        }
    }
}
