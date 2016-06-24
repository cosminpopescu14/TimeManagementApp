namespace ProiectLicenta.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;
    public partial class ProiectLicenta : DbContext
    {
        public ProiectLicenta()
            : base("name=ProiectLicenta")
        {
        }

        public virtual DbSet<Echipa_Eveniment> Echipa_Eveniment { get; set; }
        public virtual DbSet<Eveniment> Eveniments { get; set; }
        public virtual DbSet<Functie> Functies { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Task_Functie> Task_Functie { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Tip_Task> Tip_Task { get; set; }
        public virtual DbSet<Utilizatori> Utilizatoris { get; set; }
        public virtual DbSet<MembruEchipaXTask> MembruEchipaXTask { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Eveniment>()
                .Property(e => e.Denumire)
                .IsUnicode(false);

            modelBuilder.Entity<Eveniment>()
                .HasMany(e => e.Echipa_Eveniment)
                .WithOptional(e => e.Eveniment)
                .HasForeignKey(e => e.Id_Eveniment);

            modelBuilder.Entity<Eveniment>()
                .HasMany(e => e.Tasks)
                .WithOptional(e => e.Eveniment)
                .HasForeignKey(e => e.Id_Eveniment);

            modelBuilder.Entity<Functie>()
                .Property(e => e.Denumire)
                .IsUnicode(false);

            modelBuilder.Entity<Functie>()
                .HasMany(e => e.Task_Functie)
                .WithOptional(e => e.Functie)
                .HasForeignKey(e => e.Id_Functie);

            modelBuilder.Entity<Functie>()
                .HasMany(e => e.Utilizatoris)
                .WithOptional(e => e.Functie)
                .HasForeignKey(e => e.Id_Functie);

            modelBuilder.Entity<Task>()
                .HasMany(t => t.Utilizatoris)
                .WithMany(t => t.Tasks)
                .Map(m =>
                {
                    m.ToTable("MembruEchipaXTask");
                    m.MapLeftKey("Id_Task");
                    m.MapRightKey("Id_Utilizator");
                });

            modelBuilder.Entity<Task>()
                .Property(e => e.Descriere_Suplimentara)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.Stare_Task)
                .IsUnicode(false);

            modelBuilder.Entity<Tip_Task>()
                .Property(e => e.Denumire)
                .IsUnicode(false);

            modelBuilder.Entity<Tip_Task>()
                .HasMany(e => e.Task_Functie)
                .WithOptional(e => e.Tip_Task)
                .HasForeignKey(e => e.Id_Tip_Task);

            modelBuilder.Entity<Tip_Task>()
                .HasMany(e => e.Tasks)
                .WithOptional(e => e.Tip_Task)
                .HasForeignKey(e => e.Id_Tip_Task);

            modelBuilder.Entity<Utilizatori>()
                .Property(e => e.Nume_Utilizator)
                .IsUnicode(false);

            modelBuilder.Entity<Utilizatori>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Utilizatori>()
                .Property(e => e.Parola)
                .IsUnicode(false);

            modelBuilder.Entity<Utilizatori>()
                .HasMany(e => e.Echipa_Eveniment)
                .WithOptional(e => e.Utilizatori)
                .HasForeignKey(e => e.Id_Utilizator);
        }
    }
}
