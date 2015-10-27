using Library.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BorrowController : Controller
    {
        public paradiseContext context = new paradiseContext();


        // GET: Borrow
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getAllBorrows()
        {
            return Json(getAllBorrowsDB(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getBorrow(int seqNum)
        {
            return Json(getBorrowData(seqNum), JsonRequestBehavior.AllowGet);
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
            return Json(getBorrowHistoryForUser(userId), JsonRequestBehavior.AllowGet);
        }


        public JsonResult getOpenBorrows(string userId)
        {
            return Json(getOpenBorrowsForUser(userId), JsonRequestBehavior.AllowGet);
        }

        public bool borrowBook(int bookId)
        {
            Book book = new BooksController().getBookByIdDB(bookId);
            if (book.copies <= 0)
            {
                return false;
            }
            book.copies--;
            return (new BooksController()).updateBook(book);
        }

        public bool returnBook(Book book)
        {
            book.copies++;
            return (new BooksController()).updateBook(book);
        }

        public JsonResult returnBorrow(int borrowSeq)
        {
            string message = "";
            try
            {
                Borrow borrow = getBorrowById(borrowSeq);
                borrow.ReturnDate = DateTime.Today;
                Book book = new BooksController().getBookByIdDB(borrow.bookId);
                returnBook(book); 
                updateBorrow(borrow);

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
            if (!deleteBorrowDB(borrowSeq)){
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
                Borrower currentBorrower = new BorrowerController().getBorrowerByUserID(oId.ToString());
                if (currentBorrower != null)
                {
                    Borrow newBorrow = new Borrow();
                    newBorrow.bookId = bookId;
                    newBorrow.borrowDate = DateTime.Today;
                    newBorrow.borrowerId = currentBorrower.id;

                    if (!borrowBook(bookId))
                    {
                        message = "there are no available copies of this book";
                    }
                    else if (!addNewBorrow(newBorrow))
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

        public Borrow getBorrowById(int id)
        {
            return (context.borrows.Find(id));
        }

        public IEnumerable<object> getOpenBorrowsForUser(string userId)
        {

            var borrows = (from borrow in context.borrows
                           where (borrow.borrower.user.id == userId &&
                                 borrow.ReturnDate == null)
                           select new
                           {
                               borrowSeqNumber = borrow.seqNumber,
                               borrowerFirstName = borrow.borrower.firstName,
                               borrowerLastName = borrow.borrower.lastName,
                               borrowerPhone = borrow.borrower.phone,
                               borrowerMail = borrow.borrower.mail,
                               borrowerAddress = borrow.borrower.address,
                               bookTitle = borrow.book.title,
                               bookAuthor = borrow.book.author,
                               borrowDate = borrow.borrowDate
                           })
                             .ToList()
                             .Select(x => new
                             {
                                 borrowSeqNumber = x.borrowSeqNumber,
                                 borrowerFirstName = x.borrowerFirstName,
                                 borrowerLastName = x.borrowerLastName,
                                 borrowerPhone = x.borrowerPhone,
                                 borrowerMail = x.borrowerMail,
                                 borrowerAddress = x.borrowerAddress,
                                 bookTitle = x.bookTitle,
                                 bookAuthor = x.bookAuthor,
                                 borrowDate = x.borrowDate.ToString("dd/MM/yyyy")
                             });

            return borrows;
        }

        public IEnumerable<object> getBorrowHistoryForUser(string userID)
        {

            var borrows = (from borrow in context.borrows
                           where (borrow.borrower.user.id == userID &&
                                 borrow.ReturnDate != null)
                           select new
                           {
                               borrowSeqNumber = borrow.seqNumber,
                               borrowerId = borrow.borrower.id,
                               borrowerFirstName = borrow.borrower.firstName,
                               borrowerLastName = borrow.borrower.lastName,
                               borrowerPhone = borrow.borrower.phone,
                               borrowerMail = borrow.borrower.mail,
                               borrowerAddress = borrow.borrower.address,
                               bookId = borrow.book.id,
                               bookTitle = borrow.book.title,
                               bookAuthor = borrow.book.author,
                               borrowDate = borrow.borrowDate
                           })
                             .ToList()
                             .Select(x => new
                             {
                                 borrowSeqNumber = x.borrowSeqNumber,
                                 borrowerId = x.borrowerId,
                                 borrowerFirstName = x.borrowerFirstName,
                                 borrowerLastName = x.borrowerLastName,
                                 borrowerPhone = x.borrowerPhone,
                                 borrowerMail = x.borrowerMail,
                                 borrowerAddress = x.borrowerAddress,
                                 bookId = x.bookId,
                                 bookTitle = x.bookTitle,
                                 bookAuthor = x.bookAuthor,
                                 borrowDate = x.borrowDate.ToString("dd/MM/yyyy")
                             });

            return borrows;
        }

        public object getBorrowData(int seqNum)
        {

            var borrows = (from borrow in context.borrows
                           where (borrow.seqNumber == seqNum)
                           select new
                           {
                               borrowSeqNumber = borrow.seqNumber,
                               borrowerId = borrow.borrower.id,
                               borrowerFirstName = borrow.borrower.firstName,
                               borrowerLastName = borrow.borrower.lastName,
                               borrowerPhone = borrow.borrower.phone,
                               borrowerMail = borrow.borrower.mail,
                               borrowerAddress = borrow.borrower.address,
                               bookId = borrow.book.id,
                               bookTitle = borrow.book.title,
                               bookAuthor = borrow.book.author,
                               borrowDate = borrow.borrowDate,
                               returnDate = borrow.ReturnDate
                           })
                             .ToList()
                             .Select(x => new
                             {
                                 borrowSeqNumber = x.borrowSeqNumber,
                                 borrowerId = x.borrowerId,
                                 borrowerFirstName = x.borrowerFirstName,
                                 borrowerLastName = x.borrowerLastName,
                                 borrowerPhone = x.borrowerPhone,
                                 borrowerMail = x.borrowerMail,
                                 borrowerAddress = x.borrowerAddress,
                                 bookId = x.bookId,
                                 bookTitle = x.bookTitle,
                                 bookAuthor = x.bookAuthor,
                                 borrowDate = x.borrowDate.ToString("dd/MM/yyyy"),
                                 returnDate = (x.returnDate.HasValue ? x.returnDate.Value.ToString("MM/dd/yyyy") : string.Empty)
                             });

            return borrows.FirstOrDefault();
        }

        public  IEnumerable<object> getAllBorrowsDB()
        {

            var borrows = (from borrow in context.borrows
                           select new
                           {
                               borrowSeqNumber = borrow.seqNumber,
                               borrowerId = borrow.borrower.id,
                               borrowerFirstName = borrow.borrower.firstName,
                               borrowerLastName = borrow.borrower.lastName,
                               bookId = borrow.book.id,
                               bookTitle = borrow.book.title,
                               bookAuthor = borrow.book.author,
                               borrowDate = borrow.borrowDate,
                               returnDate = borrow.ReturnDate

                           })
                             .ToList()
                             .Select(x => new
                             {
                                 borrowSeqNumber = x.borrowSeqNumber,
                                 borrowerId = x.borrowerId,
                                 borrowerFirstName = x.borrowerFirstName,
                                 borrowerLastName = x.borrowerLastName,
                                 bookId = x.bookId,
                                 bookTitle = x.bookTitle,
                                 bookAuthor = x.bookAuthor,
                                 borrowDate = x.borrowDate.ToString("dd/MM/yyyy"),
                                 returnDate = (x.returnDate.HasValue ? x.returnDate.Value.ToString("MM/dd/yyyy") : string.Empty)
                             });

            return borrows;
        }

        public bool updateBorrow(Borrow borrow)
        {
            try
            {
                context.Entry(borrow).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool addNewBorrow(Borrow borrow)
        {
            try
            {
                context.borrows.Add(borrow);
                context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool deleteBorrowDB(int seq)
        {
            try
            {
                Borrow b = getBorrowById(seq);

                context.borrows.Remove(b);
                context.SaveChanges();
            }
            catch
            {
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