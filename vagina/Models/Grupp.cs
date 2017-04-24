﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Systemet.Models
{
    public class Grupp
    {

        [Key]
        public int GruppID { get; set; }

        [Required(ErrorMessage = "Gruppnamn måste anges.")]
        public string GruppNamn { get; set; }


        public List<AnvändarKonton> GruppMedlemmar { get; set; }
    }
}