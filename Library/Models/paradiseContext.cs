using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class paradiseContext : DbContext
    {
        public paradiseContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Book> books { get; set; }
        public DbSet<Borrower> borrowers { get; set; }
        public DbSet<Borrow> borrows { get; set; }
        public DbSet<User> users { get; set; }
    }

}