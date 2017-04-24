﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Systemet.Models
{
    public class AnvändarKonton
    {
        [Key]
        public int AnvändarID { get; set; }

        [Required(ErrorMessage = "förnamn måste anges.")]
        public string FörNamn { get; set; }

        [Required(ErrorMessage = "efternamn måste anges.")]
        public string EfterNamn { get; set; }

        [Required(ErrorMessage = "Email måste anges.")]
        //[RegularExpression("")]
        public string Email { get; set; }

        public int Telefon { get; set; }

        [Required(ErrorMessage = "lösenord måste anges.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "bekräfta ditt lösenord.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        public List<Grupp> TillhörGrupper { get; set; }
    }
}