using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Borrow
    {
        [Key]
        [DisplayName("Seq Number")]
        public int seqNumber { get; set; }
        [DisplayName("Book")]
        [Required]
        public Book book { get; set; }
        [DisplayName("Borrower")]
        [Required]
        public Borrower borrower { get; set; }
        [DisplayName("Borrow Date")]
        [Required]
        public DateTime borrowDate { get; set; }
        [DisplayName("Retuen Date")]
        [Required]
        public DateTime ReturnDate { get; set; }
       
    }
}