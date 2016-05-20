namespace ProiectLicenta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Eveniment")]
    public partial class Eveniment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Eveniment()
        {
            Echipa_Eveniment = new HashSet<Echipa_Eveniment>();
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }

        [StringLength(500)]
        public string Denumire { get; set; }

        public DateTime? Data_Start { get; set; }

        public DateTime? Data_Sfarsit { get; set; }

        public bool? Activ { get; set; }

        public int? Id_MO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Echipa_Eveniment> Echipa_Eveniment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
