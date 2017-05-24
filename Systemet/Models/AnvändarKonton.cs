using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Systemet.Models
{
    public class AnvändarKonton
    {

        public AnvändarKonton()
        {
            this.TillhörGrupper = new HashSet<Grupp>();
        }

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

        //public virtual Grupp grupper { get; set; }

        public virtual ICollection<Grupp> TillhörGrupper { get; set; }

        public virtual ICollection<Uppgifter> AnsvararFörUppgift { get; set; }

        public virtual ICollection<EvenemangsKommentarer> Kommentarer { get; set; }


    }
}