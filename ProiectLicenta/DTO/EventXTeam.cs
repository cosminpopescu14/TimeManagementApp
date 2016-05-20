using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProiectLicenta.DTO
{
    public class EventXTeam
    {
        public int Id_Eveniment { get; set; }
        public int[] Id_Utilizator { get; set; }
        public string Denumire { get; set; }
        public string Nume_Utilizator { get; set; }
        public string Email { get; set; }
    }
}