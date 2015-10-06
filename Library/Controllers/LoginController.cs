using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            string id = this.Request["id"];
            string password = this.Request.Params["password"];

            if (id != null && password != null){
                /*Query From users table*/
                HttpContext.Session.Add("Connected", id);
            }


            if (HttpContext.Session["Connected"] != null && HttpContext.Session["Connected"] != string.Empty){
                return View("../Home/Index", "_Layout");
            }

            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}