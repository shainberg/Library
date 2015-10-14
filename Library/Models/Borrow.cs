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

        public Borrow getBorrowById(int id)
        {
            paradiseContext context = new paradiseContext();
            return (context.borrows.Find(id));
        }

        public IEnumerable<object> getOpenBorrowByUserID(int id)
        {
            paradiseContext context = new paradiseContext();

            var borrows = (from borrow in context.borrows
                           where (borrow.borrower.user.id == id &&
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

        public IEnumerable<object> getBorrowHistoryByUserID(int id)
        {
            paradiseContext context = new paradiseContext();

            var borrows = (from borrow in context.borrows
                           where (borrow.borrower.user.id == id &&
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

        public IEnumerable<object> getAllBorrows()
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

        public bool updateBorrow(Borrow borrow)
        {
            paradiseContext context = new paradiseContext();
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
    }
}