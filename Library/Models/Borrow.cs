using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Borrow
    {
        [Key]
        [DisplayName("Seq Number")]
        public int seqNumber { get; set; }
        [DisplayName("BookId")]
        [Required]
        public int bookId { get; set; }
        [ForeignKey("bookId")]
        public Book book { get; set; }
        [DisplayName("BorrowerId")]
        [Required]
        public int borrowerId { get; set; }
        [ForeignKey("borrowerId")]
        public Borrower borrower { get; set; }
        [DisplayName("Borrow Date")]
        [Required]
        public DateTime borrowDate { get; set; }
        [DisplayName("Retuen Date")]
        public DateTime? ReturnDate { get; set; }
 
        public static Borrow getBorrowById(int id)
        {
            paradiseContext context = new paradiseContext();
            return (context.borrows.Find(id));
        }

        public static IEnumerable<object> getOpenBorrowsForUser(string userId)
        {
            paradiseContext context = new paradiseContext();

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

        public static IEnumerable<object> getBorrowHistoryForUser(string userID)
        {
            paradiseContext context = new paradiseContext();

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

        public static IEnumerable<object> getAllBorrows()
        {
            paradiseContext context = new paradiseContext();

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

        public bool updateBorrow()
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

        public bool addNewBorrow(){
            try
            {
                paradiseContext context = new paradiseContext();
                context.borrows.Add(this);
                context.SaveChanges();
            }
            catch {
                return false;
            }

            return true;
        }

        public static bool deleteBorrow(int seq)
        {
            try
            {
                paradiseContext context = new paradiseContext();

                Borrow b = context.borrows.Find(seq);
            
                context.borrows.Remove(b);
                context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}