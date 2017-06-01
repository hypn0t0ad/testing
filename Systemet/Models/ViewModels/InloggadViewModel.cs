using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Systemet.Models.ViewModels
{
    public class InloggadViewModel
    {
        public virtual AnvändarKonton användare { get; set; }
        public virtual List<Grupp> Grupperna { get; set; }
        public virtual List<Evenemang> Evenemangen { get; set; }
        public virtual List<Uppgifter> uppgifterna { get; set; }
        public virtual List<GruppFörfrågan> ansökningarna { get; set; }

        public virtual List<string> allagrupper { get; set; }
    }
}