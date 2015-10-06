using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class pContext : DbContext
    {
        public DbSet<Book> books { get; set; }
        public DbSet<Borrower> borrowers { get; set; }
        public DbSet<Borrow> borrows { get; set; }
    }

}