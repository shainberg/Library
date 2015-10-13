using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BorrowerController : Controller
    {
        Borrower b = new Borrower();

        // GET: Readers
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getCurrentBorrower()
        {
            object oId = this.Session["connected"];
            if (oId != null)
            {
                return getBorrowerByUserId(oId.ToString());
            }

            return null;
        }

        public JsonResult getAllBorrowers()
        {
            return Json(b.getAllBorrowers(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getBorrowerByUserId(string userId)
        {
            return Json(b.getBorrowerByUserID(userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult updateBorrower([Bind(Include = "id,userId,firstName,lastName,sex,phone,address,mail")] Borrower borrower)
        {
            string message = "";
            if (ModelState.IsValid) {
                if (!b.updateBorrower(borrower))
                {
                    message = "there's a problem.... try again later";
                }
            }
            else
            {
                message = "the borrower's parameters are not valid";
            }

            return (Json(message, JsonRequestBehavior.AllowGet));
        }
    }
}