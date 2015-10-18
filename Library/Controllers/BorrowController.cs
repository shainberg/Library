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
        // GET: Borrow
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getAllBorrows()
        {
            return Json(Borrow.getAllBorrows(), JsonRequestBehavior.AllowGet);
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
            return Json(Borrow.getBorrowHistoryForUser(userId), JsonRequestBehavior.AllowGet);
        }


        public JsonResult getOpenBorrows(string userId)
        {
            return Json(Borrow.getOpenBorrowsForUser(userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult returnBorrow(int borrowSeq)
        {
            string message = "";
            try
            {
                Borrow borrow = Borrow.getBorrowById(borrowSeq);
                borrow.ReturnDate = DateTime.Today;
                Book book = Book.getBookByID(borrow.bookId);
                book.returnBook();
                borrow.updateBorrow();

            }
            catch
            {
                message = "there's a problem.... try again later";
            }
            return (Json(message, JsonRequestBehavior.AllowGet));

        }

        public JsonResult deleteBorrow(int borrowSeq)
        {
            string message = "";
            if (!Borrow.deleteBorrow(borrowSeq)){
                message = "there's a problem.... try again later";
            }
            return (Json(message, JsonRequestBehavior.AllowGet));
        }

        public JsonResult createBorrow(int bookId)
        {
            string message = "";
            object oId = this.Session["connected"];
            if (oId != null)
            {
                Borrower currentBorrower = Borrower.getBorrowerByUserID(oId.ToString());
                if (currentBorrower != null)
                {
                    Borrow newBorrow = new Borrow();
                    newBorrow.bookId = bookId;
                    newBorrow.borrowDate = DateTime.Today;
                    newBorrow.borrowerId = currentBorrower.id;

                    if (!newBorrow.addNewBorrow())
                    {
                        message = "there's a problem.... try again later";
                    }
                }
                else
                {
                    message = "current user isn't attached to any borrower!!!";
                }
            }
            else
            {
                message = "there is no user connected";
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}