using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.DTO
{
    public class Tasks
    {
        public int Id { get; set; }

        [Required]
        public int Id_Tip_Task { get; set; }

        [Required]
        public int Id_Eveniment { get; set; }

        [Required]
        public string Descriere_Suplimentara { get; set; }

        [Required]
        public DateTime Termen { get; set; }

        [Required]
        public DateTime Data_Creare_Task { get; set; }

        [Required]
        public DateTime Data_Sfarsit_Task { get; set; }

        public string Stare_Task { get; set; }
        public bool Activ { get; set; }
    }
}