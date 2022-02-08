using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ApiHelpDents.Domain.Entities;

#nullable disable

namespace ApiHelpDents.Infraestructure.Data
{
    public partial class HelpDentsBDContext : DbContext
    {
        public HelpDentsBDContext()
        {
        }

        public HelpDentsBDContext(DbContextOptions<HelpDentsBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administradors { get; set; }
        public virtual DbSet<Asesor> Asesors { get; set; }
        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Especialidad> Especialidads { get; set; }
        public virtual DbSet<Turno> Turnos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(" Data Source = JONA; Initial Catalog=HelpDentsBD; Integrated Security=true;");
            }
        } */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.IdAdmin)
                    .HasName("PK_idAdmin");

                entity.ToTable("Administrador");

                entity.Property(e => e.IdAdmin).HasColumnName("idAdmin");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Asesor>(entity =>
            {
                entity.HasKey(e => e.IdAsesor)
                    .HasName("PK_idAsesor");

                entity.ToTable("Asesor");

                entity.Property(e => e.IdAsesor).HasColumnName("idAsesor");

                entity.Property(e => e.ClaveEsp).HasColumnName("claveEsp");

                entity.Property(e => e.ClaveTurno).HasColumnName("claveTurno");

                entity.Property(e => e.ClaveUsuario).HasColumnName("claveUsuario");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Facebook)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Instagram)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Twitter)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Youtube)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClaveEspNavigation)
                    .WithMany(p => p.Asesors)
                    .HasForeignKey(d => d.ClaveEsp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Esp");

                entity.HasOne(d => d.ClaveTurnoNavigation)
                    .WithMany(p => p.Asesors)
                    .HasForeignKey(d => d.ClaveTurno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Turno");

                entity.HasOne(d => d.ClaveUsuarioNavigation)
                    .WithMany(p => p.Asesors)
                    .HasForeignKey(d => d.ClaveUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario");
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.IdComentario)
                    .HasName("PK_idComentario");

                entity.ToTable("Comentario");

                entity.Property(e => e.IdComentario).HasColumnName("idComentario");

                entity.Property(e => e.ClaveAsesor).HasColumnName("claveAsesor");

                entity.Property(e => e.ClaveUsuario).HasColumnName("claveUsuario");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.ClaveAsesorNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.ClaveAsesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComentarioAsesor");

                entity.HasOne(d => d.ClaveUsuarioNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.ClaveUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComentarioUsuario");
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.IdEsp)
                    .HasName("PK_idEsp");

                entity.ToTable("Especialidad");

                entity.Property(e => e.IdEsp).HasColumnName("idEsp");

                entity.Property(e => e.NombreEsp)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Turno>(entity =>
            {
                entity.HasKey(e => e.IdTurno)
                    .HasName("PK_idTurno");

                entity.ToTable("Turno");

                entity.Property(e => e.IdTurno).HasColumnName("idTurno");

                entity.Property(e => e.NombreTurno)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_idUsuario");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
