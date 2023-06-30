using System;
using System.Collections.Generic;
using ApiParcial.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiParcial.Data;

public partial class ContextBD : DbContext
{
    public ContextBD()
    {
    }

    public ContextBD(DbContextOptions<ContextBD> options)
        : base(options)
    {
    }

    public virtual DbSet<Avione> Aviones { get; set; }

    public virtual DbSet<Fabricante> Fabricantes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=138.99.7.66;Port=5432;Database=prog3_aerolineas;User Id=prog3_viernes;Password=98745Tup");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Aviones_pk");

            entity.HasIndex(e => e.IdFabricante, "fki_fabricante");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.IdFabricanteNavigation).WithMany(p => p.Aviones)
                .HasForeignKey(d => d.IdFabricante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fabricante");
        });

        modelBuilder.Entity<Fabricante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Fabricantes_pk");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Usuarios_pk");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
