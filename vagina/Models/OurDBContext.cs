using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace vagina.Models
{
    public class OurDBContext : DbContext
    {
        public DbSet<UserAccount> UserAccount { get; set; }
    }
}