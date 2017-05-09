﻿using System;
using System.Collections.Generic;
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
        public string GruppNamn { get; set; }

        public int LedareID { get; set; }

        //public virtual AnvändarKonton användare { get; set; }


        public virtual ICollection<AnvändarKonton> GruppMedlemmar { get; set; }
        
    }
}