using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PruebaCristianArroyo.Models;

public partial class PruebaCristianContext : DbContext
{
    public PruebaCristianContext()
    {
    }

    public PruebaCristianContext(DbContextOptions<PruebaCristianContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carta> Cartas { get; set; }

    public virtual DbSet<Tabla> Tablas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=LAP-CBARROYO\\SQLEXPRESS; database=PruebaCristian; integrated security=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cartas__3214EC079389DD82");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tabla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tabla__3214EC075B833733");

            entity.ToTable("Tabla");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
