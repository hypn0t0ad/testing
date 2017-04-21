using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace vagina.Models
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage ="Firstname is requiered.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is requiered.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is requiered.")]
        //[RegularExpression("")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is requiered.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is requiered.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please comfirm your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}