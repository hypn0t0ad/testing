using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Systemet.Models
{
    public class SystemetDBContext : DbContext
    {


        public DbSet<AnvändarKonton> konton { get; set; }

        public DbSet<Grupp> Grupps { get; set; }

        public DbSet<Evenemang> Evenemangs { get; set; }

        public DbSet<Uppgifter> Uppgifters { get; set; }

        public DbSet<EvenemangsKommentarer> EvenemangsKommentarers { get; set; }

        public DbSet<GruppFörfrågan> GruppFörfrågan { get; set; }
    }
}