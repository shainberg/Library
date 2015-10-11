using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Borrower
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string firstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string lastName { get; set; }
        [DisplayName("Sex")]
        [Required]
        [StringLength(1)]
        public string sex { get; set; }
        [DisplayName("Phone")]
        [Required]
        public string phone { get; set; }
        [DisplayName("Address")]
        [Required]
        public string address { get; set; }
        [DisplayName("Mail")]
        public string mail { get; set; }
        [DisplayName("User")]
        public int userId { get; set; } 
        [ForeignKey("userId")]
        public User user { get; set; }

        public Boolean addNewBook()
        {
            paradiseContext context = new paradiseContext();
            try
            {
                context.borrowers.Add(this);
                context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean deleteBorrower(String id)
        {
            Boolean answer = false;
            paradiseContext context = new paradiseContext();

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

        public IEnumerable<Borrower> getAllBorrowers()
        {
            paradiseContext context = new paradiseContext();
            return (context.borrowers.ToList<Borrower>());
        }

        public Borrower getBorrowerByUserID(int id)
        {
            paradiseContext context = new paradiseContext();
            return (context.borrowers.FirstOrDefault(m => m.user.id == id));
        }


    }
}