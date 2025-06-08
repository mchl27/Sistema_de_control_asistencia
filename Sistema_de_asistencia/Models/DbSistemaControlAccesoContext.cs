using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sistema_de_asistencia.Models;

public partial class DbSistemaControlAccesoContext : DbContext
{
    public DbSistemaControlAccesoContext()
    {
    }

    public DbSistemaControlAccesoContext(DbContextOptions<DbSistemaControlAccesoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=Michael\\SQLEXPRESS;Database=DB_SistemaControlAcceso;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estudian__3214EC078BD967E3");

            entity.ToTable("Estudiante");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Curso).HasMaxLength(50);
            entity.Property(e => e.Grado).HasMaxLength(50);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Estudiante)
                .HasForeignKey<Estudiante>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estudiante__Id__4CA06362");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Profesor__3214EC0743F0595B");

            entity.ToTable("Profesor");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Departamento).HasMaxLength(100);
            entity.Property(e => e.Especialidad).HasMaxLength(100);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Profesor)
                .HasForeignKey<Profesor>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profesor__Id__4F7CD00D");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07C0E6BFFC");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D1053446429458").IsUnique();

            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
