using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Systemet.Models
{
    public class GruppFörfrågan
    {

        [Key]
        public int FörfråganID { get; set; }
        public bool Godkänd { get; set; }
        public string text { get; set; }


        public virtual AnvändarKonton AnvändareSomFrågar { get; set; }
        public virtual Grupp GruppFörfråganGäller { get; set; }


    }
}