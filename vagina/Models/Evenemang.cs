using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Systemet.Models
{
    public class Evenemang
    {
        public int EvenemangID { get; set; }
        public string Namn { get; set; }
        public string Beskrivning { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime Dag { get; set; }
        public List<string> kommentarer { get; set; }


        public virtual Grupp grupp { get; set; }
    }
}