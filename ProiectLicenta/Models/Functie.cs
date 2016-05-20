namespace ProiectLicenta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Functie")]
    public partial class Functie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Functie()
        {
            Task_Functie = new HashSet<Task_Functie>();
            Utilizatoris = new HashSet<Utilizatori>();
        }

        public int Id { get; set; }

        [StringLength(40)]
        public string Denumire { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task_Functie> Task_Functie { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Utilizatori> Utilizatoris { get; set; }
    }
}
