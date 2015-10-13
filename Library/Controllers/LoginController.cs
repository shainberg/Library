using Library.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                paradiseContext context = new paradiseContext();
                var result = from users in context.users
                                where users.id.Equals(id) &&
                                users.password.Equals(password)
                                select users;

                if (result != null)
                {
                    User[] users = result.ToArray<User>();

                    if (users.Length == 1)
                    {
                        HttpContext.Session.Add("Connected", users[0].id);

                        if (true.Equals(users[0].isAdmin))
                        {
                            HttpContext.Session.Add("IsAdmin", true);
                        }

                        ViewBag.Error = false;
                        RedirectToAction("index", "Home");
                    }
                }
            }

            ViewBag.Error = true;
            return View("LoginNew");
        }
    }
}
