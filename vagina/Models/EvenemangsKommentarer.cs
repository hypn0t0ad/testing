using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Systemet.Models;

namespace Systemet.Models
{
    public class EvenemangsKommentarer
    {
        public int EvenemangsKommentarerID { get; set; }
        public DateTime TidenFörKommentaren { get; set; }
        public string Text { get; set; }


        public virtual Evenemang evenemang { get; set; }
    }
}