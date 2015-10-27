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

        public JsonResult deleteBook(int bookId)
        {
            string message = "";
            if (!deleteBookDB(bookId))
            {
                message = "there's a problem.... try again later";
            }
            return (Json(message, JsonRequestBehavior.AllowGet));

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

        public JsonResult getBestBooksJson()
        {
            return Json(getBestBooks(), JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<object> getBestBooks()
        {
            var books =
            from b in context.borrows
            group b by b.book into grouping
            orderby grouping.Count() descending
            select new
            {
                bookId = grouping.Key.id,
                bookTitle = grouping.Key.title,
                bookAuthor = grouping.Key.author,
                bookSeries = grouping.Key.series,
                bookNumber = grouping.Key.number,
                bookPublicationYear = grouping.Key.publicationYear,
                bookAvailableCopies = grouping.Key.copies,
                bookCount = grouping.Count()
            };

            return books;
        }

        public JsonResult EditBook([Bind(Include = "id,title,author,publisher,publicationYear,language,series,number,summery,category,copies")] Book book)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                if (!updateBook(book))
                {
                    message = "there's a problem.... try again later";
                }
            }
            else
            {
                message = "the book's parameters are not valid";
            }

            return (Json(message, JsonRequestBehavior.AllowGet));
        }


        public JsonResult createBook([Bind(Include = "title,author,publisher,publicationYear,language,series,number,summery,category,copies")] Book book)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                if (!addNewBook(book))
                {
                    message = "there's a problem.... try again later";
                }
            }
            else
            {
                message = "the book's parameters are not valid";
            }

            return (Json(message, JsonRequestBehavior.AllowGet));
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
