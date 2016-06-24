using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProiectLicenta.Models
{
    public class MembruEchipaXTask
    {
        public int Id { get; set; }

        //[ForeignKey("Id_Task")]
        public int Id_Task { get; set; }

        //[ForeignKey("Id_Utilizator")]
        public int Id_Utilizator { get; set; }

        //public virtual Task Task { get; set; }
    }
}