using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Systemet.Models
{
    public class OurDBContext : DbContext
    {
        public DbSet<AnvändarKonton> konton { get; set; }

        public System.Data.Entity.DbSet<Systemet.Models.Grupp> Grupps { get; set; }
    }
}