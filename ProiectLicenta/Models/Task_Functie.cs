namespace ProiectLicenta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Task_Functie
    {
        public int Id { get; set; }

        public int? Id_Functie { get; set; }

        public int? Id_Tip_Task { get; set; }

        public virtual Functie Functie { get; set; }

        public virtual Tip_Task Tip_Task { get; set; }
    }
}
