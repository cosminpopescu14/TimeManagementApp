namespace ProiectLicenta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utilizatori")]
    public partial class Utilizatori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utilizatori()
        {
            Echipa_Eveniment = new HashSet<Echipa_Eveniment>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Nume_Utilizator { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Parola { get; set; }

        public int? Id_Functie { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Echipa_Eveniment> Echipa_Eveniment { get; set; }

        public virtual Functie Functie { get; set; }
    }
}
