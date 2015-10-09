using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public enum Sex
    {
        Female,
        Male
    }
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
        public Sex sex { get; set; }
        [DisplayName("Phone")]
        [Required]
        public string phone { get; set; }
        [DisplayName("Address")]
        [Required]
        public string address { get; set; }
        [DisplayName("Mail")]
        public string mail { get; set; }
        [DisplayName("User")]
        public User user { get; set; }
    }
}