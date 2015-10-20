using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class BooksController : Controller
    {

        public paradiseContext context = new paradiseContext();

        // GET: Books
        public ActionResult Index()
        {
            return View(getAllBooksDB());
        }

        public JsonResult getAllBooks()
        {
            return Json(getAllBooksDB(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getBookById(int id)
        {
            return Json(getBookByIdDB(id), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult deleteBook(int id)
        {
            string message = "";
            if (!deleteBookDB(id))
            {
                message = "there's a problem.... try again later";
            }
            return (Json(message, JsonRequestBehavior.AllowGet));

        }

        

        public bool borrowBook(Book book)
        {
            bool answer = false;

            if (book.copies > 0)
            {
                book.copies--;
                if (updateBook(book))
                {
                    answer = true;
                }
            }

            return (answer);
        }

        public  Boolean addNewBook(Book book)
        {
            try
            {
                context.books.Add(book);
                context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool updateBook(Book book)
        {
            try
            {
                context.Entry(book).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public  Boolean deleteBookDB(int bookId)
        {
            try
            {
                Boolean answer = false;

                Book book = context.books.Find(bookId);

                context.books.Remove(book);

                answer = (context.SaveChanges() > 0);

                return (answer);

            }
            catch
            {
                return false;
            }

        }

        public  IEnumerable<Book> getAllBooksDB()
        {
            return (context.books.ToList<Book>());
        }

        public Book getBookByIdDB(int id)
        {
            return (context.books.Find(id));
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
