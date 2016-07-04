using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProiectLicenta.DTO
{
    public class TaskXUser
    {
        public int Id { get; set; }
        public string Nume_Utilizator { get; set; }
        public string Rol { get; set; }
        public string Descriere_Suplimentara { get; set; }
        public DateTime Data_Creare_Task { get; set; }
        public DateTime Data_Sfarsit_Task { get; set; }
        public string Stare_Task { get; set; }
    }
}