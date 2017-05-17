using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Systemet.Models;

namespace Systemet.Models
{
    public class Evenemang
    {
        public int EvenemangID { get; set; }
        public string Namn { get; set; }
        public string Beskrivning { get; set; }
        public DateTime Tidpunkt { get; set; }
        public ICollection<EvenemangsKommentarer> Åsikter { get; set; }


        public virtual Grupp grupp { get; set; }
    }
}