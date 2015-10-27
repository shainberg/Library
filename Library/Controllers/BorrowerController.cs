using Library.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BorrowerController : Controller
    {
        public paradiseContext context = new paradiseContext();

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

        public JsonResult getBorrower(int borrowerId)
        {
            return Json(getBorrowerByIdDB(borrowerId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAllBorrowers()
        {
            return Json(getAllBorrowersDB(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getBorrowerByUserId(string userId)
        {
            return Json(getBorrowerByUserID(userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult updateBorrower([Bind(Include = "id,userId,firstName,lastName,sex,phone,address,mail")] Borrower borrower)
        {
            string message = "";
            if (ModelState.IsValid) {
                if (!updateBorrowerDB(borrower))
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

        public JsonResult createBorrower([Bind(Include = "id,userId,firstName,lastName,sex,phone,address,mail")] Borrower borrower)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                if (!addNewBorrowerDB(borrower))
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

        public JsonResult deleteBorrower(int borrowerID)
        {
            string message = "";
            if (!deleteBorrowerDB(borrowerID))
            {
                message = "there's a problem.... try again later";
            }
            return (Json(message, JsonRequestBehavior.AllowGet));

        }

        public Borrower getBorrowerByIdDB(int id)
        {
            return (context.borrowers.Find(id));
        }

        public Boolean addNewBorrowerDB(Borrower borrower)
        {
            try
            {
                context.borrowers.Add(borrower);
                context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteBorrowerDB(int id)
        {
            bool answer = false;

            Borrower borrower = context.borrowers.Find(id);
            if (borrower != null)
            {
                context.borrowers.Remove(borrower);

                //TODO:
                //var movieLink = from userMovies in context.UserMovies
                //                where userMovies.MovieID == mID
                //                select userMovies;

                //foreach (var currMovie in movieLink)
                //{
                //    context.UserMovies.Remove(currMovie);
                //}

                answer = (context.SaveChanges() > 0);
            }

            return (answer);
        }

        public  IEnumerable<Borrower> getAllBorrowersDB()
        {

            return (context.borrowers.ToList<Borrower>());
        }

        public  Borrower getBorrowerByUserID(string id)
        {
            return (context.borrowers.FirstOrDefault(m => m.user.id == id));
        }

        public bool updateBorrowerDB(Borrower borrower)
        {
            try{
                context.Entry(borrower).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch(Exception e){
                return false;
            }
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}