namespace ProiectLicenta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Echipa_Eveniment
    {
        public int Id { get; set; }

        public int? Id_Eveniment { get; set; }

        public int? Id_Utilizator { get; set; }

        public virtual Eveniment Eveniment { get; set; }

        public virtual Utilizatori Utilizatori { get; set; }
    }
}
