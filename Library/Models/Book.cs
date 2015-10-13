using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library.Models
{
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
        [StringLength(2)]
        public string language { get; set; }
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

        public bool returnBook()
        {
            this.copies++;
            return this.updateDB();
        }

        public bool borrowBook()
        {
            bool answer = false;

            if (this.copies > 0)
            {
                this.copies--;
                if (this.updateDB())
                {
                    answer = true;
                }
            }

            return (answer);
        }

        public static Boolean addNewBook(Book book)
        {
            paradiseContext context = new paradiseContext();
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

        public bool updateDB()
        {
            paradiseContext context = new paradiseContext();
            try
            {
                context.Entry(this).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }


        public Boolean deleteBook()
        {
            Boolean answer = false;
            paradiseContext context = new paradiseContext();

            context.books.Remove(this);

            //TODO:
            //var movieLink = from userMovies in context.UserMovies
            //                where userMovies.MovieID == mID
            //                select userMovies;

            //foreach (var currMovie in movieLink)
            //{
            //    context.UserMovies.Remove(currMovie);
            //}

            answer = (context.SaveChanges() > 0);

            return (answer);
        }

        public static IEnumerable<Book> getAllBooks()
        {
            paradiseContext context = new paradiseContext();
            return (context.books.ToList<Book>());
        }

        public static Book getBookByID(int id)
        {
            paradiseContext context = new paradiseContext();
            return (context.books.Find(id));
        }

    }
}