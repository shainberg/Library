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
        public Book b = new Book();

        // GET: Books
        public ActionResult Index()
        {
           

            return View(Book.getAllBooks());
        }

        public JsonResult getAllBooks()
        {
            return Json(Book.getAllBooks(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getBookById(int id)
        {
            return Json(Book.getBookByID(id), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public Boolean removeBookByID(int id)
        {
            //TODO: if current user is admin
            //if (!User.Identity.Name.Equals("") && MovieTheater.Models.User.isAdmin(User.Identity.Name))
            if(true)
            {
                Book book = Book.getBookByID(id);
                return (book.deleteBook());
            }

            return false;
        }
    }
}
