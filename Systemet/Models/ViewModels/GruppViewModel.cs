using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Systemet.Models.ViewModels
{
    public class GruppViewModel
    {
        public virtual Uppgifter uppgift { get; set; }
        public virtual Grupp grupp { get; set; }
        public virtual int ledareID { get; set; }
        public virtual int användareID { get; set; }
        

        public virtual ICollection<AnvändarKonton> medlemmar { get; set; }

        public IEnumerable<SelectListItem> lemmar { get; set; }

        


    }
}