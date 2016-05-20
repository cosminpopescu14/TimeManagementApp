using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.DTO
{
    public class Events
    {
        public int Id { get; set; }//id eveniment

        [Required]
        public string Denumire { get; set; }

        [Required]
        public DateTime Data_Start { get; set; } 

        [Required]
        public DateTime Data_Sfarsit { get; set; }

        public bool Activ { get; set; }

        public int Id_MO { get; set; } //va fi idul presedintelui de conferinta. implicit va fi egal cu id-ul userului care il adauga

    }
}