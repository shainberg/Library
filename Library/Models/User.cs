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
        public string id { get; set; }

        [DisplayName("password")]
        public string password { get; set; }

        [DisplayName("isAdmin")]
        public bool isAdmin { get; set; }

        public static Boolean isUserAdmin(string id)
        {
            paradiseContext context = new paradiseContext();
            var answer = from users in context.users
                         where users.id == id &&
                               users.isAdmin == true
                         select true;

            return (answer.Count() == 1);
        }
    }
}