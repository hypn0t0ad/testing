using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Systemet.Models
{
    public class Uppgifter
    {
        public int UppgiftID { get; set; }
        public string Namn { get; set; }
        public string Beskrivning { get; set; }
        public bool Utförd { get; set; }
        public bool Påbörjad { get; set; }
        public DateTime Startdatum { get; set; }
        public DateTime Slutdatum { get; set; }



        public virtual AnvändarKonton Ansvarig { get; set; }
        public virtual Grupp TillhörGrupp { get; set; }

    }
}