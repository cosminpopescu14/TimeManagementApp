using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;//validari asupra campurilor din clasa

namespace ProiectLicenta.DTO
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Numele de utilizator")]
        public string Nume_utilizator { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        public string Parola { get; set; }

        public int Id_Functie { get; set; }

    }
}