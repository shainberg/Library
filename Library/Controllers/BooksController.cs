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
            //Book b = new Book();
            //b.id = 1;
            //b.author = "RICK RIORDEN";
            //b.title = "percy jackson 1";
            //b.series = "percy jackson";
            //b.publicationYear = 2008;
            //b.publisher = "hdeo";
            //b.language = Language.Hebrew;
            //b.copies = 1;
            //db.books.Add(b);
            //await db.SaveChangesAsync();
           

            return View(b.getAllBooks());
        }

        public JsonResult getAllBooks()
        {
            return Json(b.getAllBooks(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getDetails(int id)
        {
            return Json(b.getBookByID(id), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public Boolean removeMovieByID(String id)
        {
            //TODO: if current user is admin
            //if (!User.Identity.Name.Equals("") && MovieTheater.Models.User.isAdmin(User.Identity.Name))
            if(true)
            {
                return (b.deleteBook(id));
            }

            return false;
        }
    }
}
