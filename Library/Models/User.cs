using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class User
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("password")]
        public string password { get; set; }

        [DisplayName("isAdmin")]
        public bool isAdmin { get; set; }
    }
}