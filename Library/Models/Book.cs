using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{

    public enum Language
    {
        Hebrew,
        English
    }
 
    public class Book
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Title")]
        [Required]
        public string title { get; set; }
        [DisplayName("Author")]
        [Required]
        public string author { get; set; }
        [DisplayName("Publisher")]
        [Required]
        public string publisher { get; set; }
        [DisplayName("Publication Year")]
        [Required]
        public int publicationYear { get; set; }
        [DisplayName("Language")]
        [Required]
        public Language language { get; set; }
        [DisplayName("Series")]
        public string series { get; set; }
        [DisplayName("Number")]
        public string number { get; set; }
        [DisplayName("Summery")]
        public string summery { get; set; }
        [DisplayName("Category")]
        public string category { get; set; }
        [DisplayName("Copies")]
        [Required]
        public int copies{ get; set; }
        [DisplayName("Picture")]
        public byte[] picture { get; set; }

        public Boolean addNewBook()
        {
            paraContext context = new paraContext();
            try
            {
                context.books.Add(this);
                context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean lendBook(int bookId)
        {
            Boolean answer = false;
            paraContext context = new paraContext();
            Book book = context.books.Find(id);

            if (book != null && book.copies > 0)
            {
                book.copies--;
                context.SaveChanges();
                answer = true;
            }

            return (answer);
        }

        public Boolean deleteBook(String id)
        {
            Boolean answer = false;
            paraContext context = new paraContext();

            Book book = context.books.Find(id);
            if (book != null)
            {
                context.books.Remove(book);

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

        public IEnumerable<Book> getAllBooks()
        {
            paraContext context = new paraContext();
            return (context.books.ToList<Book>());
        }

        public Book getBookByID(int id)
        {
            paraContext context = new paraContext();
            return (context.books.Find(id));
        }

    }
}