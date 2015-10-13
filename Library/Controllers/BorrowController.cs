using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BorrowController : Controller
    {

        Borrow b = new Borrow();
        // GET: Borrow
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getCurrentBorrowerOpenBorrows()
        {
            object oId = this.Session["connected"];
            if (oId != null)
            {
                return getOpenBorrows(oId.ToString());
            }

            return null;
        }

        public JsonResult getCurrentBorrowerHistoryBorrows()
        {
            object oId = this.Session["connected"];
            if (oId != null)
            {
                return getBorrowsHistory(oId.ToString());
            }

            return null;
        }

        public JsonResult getBorrowsHistory(string userId)
        {
            return Json(b.getBorrowHistoryByUserID(userId), JsonRequestBehavior.AllowGet);
        }


        public JsonResult getOpenBorrows(string userId)
        {
            return Json(b.getOpenBorrowByUserID(userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult returnBorrow(int borrowSeq)
        {
            string message = "";
            try
            {
                Borrow borrow = b.getBorrowById(borrowSeq);
                borrow.ReturnDate = DateTime.Today;
                Book book = Book.getBookByID(borrow.bookId);
                book.returnBook();
                borrow.updateBorrow(borrow);

            }
            catch
            {
                message = "there's a problem.... try again later";
            }
            return (Json(message, JsonRequestBehavior.AllowGet));

        }
    }
}