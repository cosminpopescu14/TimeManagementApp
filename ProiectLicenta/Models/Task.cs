namespace ProiectLicenta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Task
    {
        public int Id { get; set; }

        public int? Id_Tip_Task { get; set; }

        public int? Id_Eveniment { get; set; }

        public string Descriere_Suplimentara { get; set; }

        public DateTime? Termen { get; set; }

        public DateTime? Data_Creare_Task { get; set; }

        public DateTime? Data_Sfarsit_Task { get; set; }

        [StringLength(20)]
        public string Stare_Task { get; set; }

        public bool? Activ { get; set; }

        public virtual Eveniment Eveniment { get; set; }

        public virtual Tip_Task Tip_Task { get; set; }

        public virtual ICollection<Utilizatori> Utilizatoris { get; set; }//poate nu este bun. Avem o colectie de sarcini
    }
}
