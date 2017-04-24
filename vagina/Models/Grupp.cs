using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Systemet.Models
{
    public class Grupp
    {
        public int GruppID { get; set; }

        public string GruppNamn { get; set; }

        public List<AnvändarKonton> GruppMedlemmar { get; set; }
    }
}