using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Systemet.Models
{
    public class UppgifterViewModel
    {
        public virtual Uppgifter Uppgift { get; set; }
        public virtual Grupp Grupp { get; set; }
        public IEnumerable<SelectListItem> DropDownGrupper { get; set; }
    }

}
