using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Systemet.Models
{
    public class Grupp
    {

        public Grupp()
        {
            this.GruppMedlemmar = new HashSet<AnvändarKonton>();
        }

        [Key]
        public int GruppID { get; set; }

        [Required(ErrorMessage = "Gruppnamn måste anges.")]
        [DisplayName("Gruppnamn")]
        public string GruppNamn { get; set; }

        public int LedareID { get; set; }

        public virtual ICollection<AnvändarKonton> GruppMedlemmar { get; set; }
        public virtual ICollection<Evenemang> Evenemang { get; set; }

    }
}