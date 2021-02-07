using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2_poprawa.Models
{
    public class PetsDbContext : DbContext
    {
        public PetsDbContext() { }

        public PetsDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Volunteer_Pet> Volunteer_Pets { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }
        public virtual DbSet<BreedType> BreedTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasKey(e => e.IdPet)
                    .HasName("Pet_pk");

                entity.Property(e => e.IdBreedType)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.IsMale)
                    .IsRequired();
                entity.Property(e => e.DateRegistered)
                    .IsRequired();
                entity.Property(e => e.ApprocimateDateOfBirth)
                    .IsRequired();
                entity.HasOne(e=>e.BreedType)
                    .WithMany(e=>e.Pets)
                    .HasForeignKey(e=>e.IdBreedType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pet_BreedType");
            });

            modelBuilder.Entity<BreedType>(entity =>
            {
                entity.HasKey(e => e.IdBreedType)
                    .HasName("BreedType_pk");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.HasKey(e => e.IdVolunteer)
                    .HasName("Volunteer_pk");

                entity.Property(e => e.Name)
                    .IsRequired()

                    .HasMaxLength(80);
                entity.Property(e => e.Surname)
                    .IsRequired()

                    .HasMaxLength(80);
                entity.Property(e => e.Phone)
                    .IsRequired()

                    .HasMaxLength(30);
                entity.Property(e => e.Address)
                    .IsRequired()

                    .HasMaxLength(100);
                entity.Property(e => e.Email)
                    .IsRequired()

                    .HasMaxLength(80);
                entity.Property(e => e.StartingDate)
                    .IsRequired();

                entity.HasOne(e => e.Supervisor)
                      .WithMany(e => e.Volunteers)
                      .HasForeignKey(e => e.IdSupervisor)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("Suprevisor");
            });
            modelBuilder.Entity<Volunteer_Pet>(entity =>
            {
                entity.HasKey(e => new { e.IdVolunteer, e.IdPet })
                    .HasName("Volunteer_Pet_pk");

                entity.ToTable("Volunteer_Pet");

                entity.Property(e => e.DateAccepted)
                    .IsRequired();

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.Volunteer_Pets)
                    .HasForeignKey(d => d.IdVolunteer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Volunteer_Pet_Volunteer")
                    .IsRequired();

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Volunteer_Pets)
                    .HasForeignKey(d => d.IdPet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Volunteer_Pet_Pet")
                    .IsRequired();
            });
        }
    }
}
