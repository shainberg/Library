using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
        public string userId { get; set; } 
        [ForeignKey("userId")]
        public User user { get; set; }

        
    }
}