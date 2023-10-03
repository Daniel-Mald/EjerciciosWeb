using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Animalitos.Models.Entities;

public partial class AnimalesContext : DbContext
{
    public AnimalesContext()
    {
    }

    public AnimalesContext(DbContextOptions<AnimalesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clase> Clase { get; set; }

    public virtual DbSet<Especies> Especies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=9hn2425s2O;database=animales", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clase");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<Especies>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("especies");

            entity.HasIndex(e => e.IdClase, "fk_especie_clase_idx");

            entity.Property(e => e.Especie).HasMaxLength(45);
            entity.Property(e => e.Habitat).HasMaxLength(45);
            entity.Property(e => e.Observaciones).HasMaxLength(100);
            entity.Property(e => e.Peso).HasColumnType("double(7,2)");

            entity.HasOne(d => d.IdClaseNavigation).WithMany(p => p.Especies)
                .HasForeignKey(d => d.IdClase)
                .HasConstraintName("fk_especie_clase");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
