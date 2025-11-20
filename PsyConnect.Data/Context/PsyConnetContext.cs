using Microsoft.EntityFrameworkCore;
using PsyConnect.Core.Entities.Usuarios;
using PsyConnect.Core.Entities.Citas;
using PsyConnect.Core.Entities.Tests;
using PsyConnect.Core.Entities.Respuestas;
using PsyConnect.Core.Entities.Resultados;
using PsyConnect.Core.Entities.Recomendaciones;
using PsyConnect.Core.Entities.Certificados;
using PsyConnect.Core.Entities.Auditoría;
using PsyConnect.Core.Entities.Reportes;

namespace PsyConnect.Data.Context
{
    public class PsyConnectContext : DbContext
    {
        public PsyConnectContext(DbContextOptions<PsyConnectContext> options) : base(options)
        {
        }

        // Usuarios
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Psicólogo> Psicologos { get; set; }

        // Citas
        public DbSet<ModalidadCita> ModalidadesCita { get; set; }
        public DbSet<EstadoCita> EstadosCita { get; set; }
        public DbSet<Cita> Citas { get; set; }

        // Tests
        public DbSet<TipoTest> TiposTest { get; set; }
        public DbSet<ModalidadTest> ModalidadesTest { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<PreguntaTest> PreguntasTest { get; set; }
        public DbSet<OpcionRespuesta> OpcionesRespuesta { get; set; }

        // Respuestas
        public DbSet<EstadoRespuestaTest> EstadosRespuestaTest { get; set; }
        public DbSet<RespuestaTest> RespuestasTest { get; set; }
        public DbSet<DetalleRespuestaTest> DetallesRespuestaTest { get; set; }

        // Resultados
        public DbSet<ResultadoInterpretacion> ResultadosInterpretacion { get; set; }

        // Recomendaciones
        public DbSet<RecomendacionPersonalizada> RecomendacionesPersonalizadas { get; set; }

        // Certificados
        public DbSet<Certificado> Certificados { get; set; }

        // Auditoría
        public DbSet<Historico> Historicos { get; set; }

        // Reportes
        public DbSet<Reporte> Reportes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioID);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Contraseña).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.TipoUsuario).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Teléfono).HasMaxLength(20);
                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Estado).HasDefaultValue(true);

                entity.HasIndex(e => e.Email).IsUnique();
            });


            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.EstudianteID);
                entity.Property(e => e.Matrícula).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Carrera).HasMaxLength(100);
                entity.Property(e => e.Género).HasMaxLength(20);
                entity.Property(e => e.Dirección).HasMaxLength(255);

                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.UsuarioID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.Matrícula).IsUnique();
            });


            modelBuilder.Entity<Psicólogo>(entity =>
            {
                entity.HasKey(e => e.PsicólogoID);
                entity.Property(e => e.Cédula).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Especialidad).HasMaxLength(100);
                entity.Property(e => e.Licencia).HasMaxLength(100);
                entity.Property(e => e.SedeAsignada).HasMaxLength(100).HasDefaultValue("Fraternidad");

                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.UsuarioID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.Cédula).IsUnique();
            });


            modelBuilder.Entity<ModalidadCita>(entity =>
            {
                entity.HasKey(e => e.ModalidadID);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descripción).HasMaxLength(255);

                entity.HasIndex(e => e.Nombre).IsUnique();
            });


            modelBuilder.Entity<EstadoCita>(entity =>
            {
                entity.HasKey(e => e.EstadoID);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);

                entity.HasIndex(e => e.Nombre).IsUnique();
            });


            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(e => e.CitaID);
                entity.Property(e => e.Ubicación).HasMaxLength(255);
                entity.Property(e => e.EnlaceTeams).HasMaxLength(500);
                entity.Property(e => e.NotasEstudiante).HasColumnType("nvarchar(max)");
                entity.Property(e => e.ObservacionesPsicólogo).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Duración).HasDefaultValue(60);
                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Estudiante)
                    .WithMany(s => s.Citas)
                    .HasForeignKey(e => e.EstudianteID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Psicólogo)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(e => e.PsicólogoID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ModalidadCita)
                    .WithMany(m => m.Citas)
                    .HasForeignKey(e => e.ModalidadID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.EstadoCita)
                    .WithMany(s => s.Citas)
                    .HasForeignKey(e => e.EstadoID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.FechaHora);
            });


            modelBuilder.Entity<TipoTest>(entity =>
            {
                entity.HasKey(e => e.TipoTestID);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripción).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Activo).HasDefaultValue(true);

                entity.HasIndex(e => e.Nombre).IsUnique();
            });


            modelBuilder.Entity<ModalidadTest>(entity =>
            {
                entity.HasKey(e => e.ModalidadTestID);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);

                entity.HasIndex(e => e.Nombre).IsUnique();
            });


            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.TestID);
                entity.Property(e => e.NombreTest).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Descripción).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Activo).HasDefaultValue(true);
                entity.Property(e => e.FechaCreación).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.TipoTest)
                    .WithMany(t => t.Tests)
                    .HasForeignKey(e => e.TipoTestID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ModalidadTest)
                    .WithMany(m => m.Tests)
                    .HasForeignKey(e => e.ModalidadTestID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Psicólogo)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(e => e.PsicólogoID)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<PreguntaTest>(entity =>
            {
                entity.HasKey(e => e.PreguntaID);
                entity.Property(e => e.Texto).IsRequired().HasColumnType("nvarchar(max)");
                entity.Property(e => e.Tipo).HasMaxLength(50).HasDefaultValue("Opción Múltiple");

                entity.HasOne(e => e.Test)
                    .WithMany(t => t.PreguntasTest)
                    .HasForeignKey(e => e.TestID)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<OpcionRespuesta>(entity =>
            {
                entity.HasKey(e => e.OpcionID);
                entity.Property(e => e.Texto).IsRequired().HasMaxLength(255);

                entity.HasOne(e => e.PreguntaTest)
                    .WithMany(p => p.OpcionesRespuesta)
                    .HasForeignKey(e => e.PreguntaID)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<EstadoRespuestaTest>(entity =>
            {
                entity.HasKey(e => e.EstadoID);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);

                entity.HasIndex(e => e.Nombre).IsUnique();
            });


            modelBuilder.Entity<RespuestaTest>(entity =>
            {
                entity.HasKey(e => e.RespuestaID);
                entity.Property(e => e.FechaInicio).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Estudiante)
                    .WithMany(s => s.RespuestasTest)
                    .HasForeignKey(e => e.EstudianteID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Test)
                    .WithMany(t => t.RespuestasTest)
                    .HasForeignKey(e => e.TestID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.EstadoRespuestaTest)
                    .WithMany(s => s.RespuestasTest)
                    .HasForeignKey(e => e.EstadoID)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<DetalleRespuestaTest>(entity =>
            {
                entity.HasKey(e => e.DetalleID);

                entity.HasOne(e => e.RespuestaTest)
                    .WithMany(r => r.DetallesRespuesta)
                    .HasForeignKey(e => e.RespuestaID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PreguntaTest)
                    .WithMany(p => p.DetallesRespuesta)
                    .HasForeignKey(e => e.PreguntaID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.OpcionRespuesta)
                    .WithMany(o => o.DetallesRespuesta)
                    .HasForeignKey(e => e.OpcionSeleccionada)
                    .OnDelete(DeleteBehavior.SetNull);
            });


            modelBuilder.Entity<ResultadoInterpretacion>(entity =>
            {
                entity.HasKey(e => e.ResultadoID);
                entity.Property(e => e.Interpretación).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Recomendación).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Nivel).HasMaxLength(50);

                entity.HasOne(e => e.RespuestaTest)
                    .WithOne(r => r.ResultadoInterpretacion)
                    .HasForeignKey<ResultadoInterpretacion>(e => e.RespuestaID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Psicólogo)
                    .WithMany(p => p.ResultadosInterpretacion)
                    .HasForeignKey(e => e.PsicólogoID)
                    .OnDelete(DeleteBehavior.SetNull);
            });


            modelBuilder.Entity<RecomendacionPersonalizada>(entity =>
            {
                entity.HasKey(e => e.RecomendacionID);
                entity.Property(e => e.Título).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Descripción).HasColumnType("nvarchar(max)");
                entity.Property(e => e.TipoRecurso).HasMaxLength(50);
                entity.Property(e => e.URL).HasMaxLength(500);
                entity.Property(e => e.FechaAsignación).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Vigente).HasDefaultValue(true);

                entity.HasOne(e => e.Estudiante)
                    .WithMany(s => s.RecomendacionesPersonalizadas)
                    .HasForeignKey(e => e.EstudianteID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Psicólogo)
                    .WithMany(p => p.RecomendacionesPersonalizadas)
                    .HasForeignKey(e => e.PsicólogoID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ResultadoInterpretacion)
                    .WithMany(r => r.RecomendacionesPersonalizadas)
                    .HasForeignKey(e => e.ResultadoID)
                    .OnDelete(DeleteBehavior.SetNull);
            });


            modelBuilder.Entity<Certificado>(entity =>
            {
                entity.HasKey(e => e.CertificadoID);
                entity.Property(e => e.TipoCertificado).HasMaxLength(50);
                entity.Property(e => e.RutaArchivo).HasMaxLength(500);
                entity.Property(e => e.FechaGeneración).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Descargado).HasDefaultValue(false);

                entity.HasOne(e => e.Estudiante)
                    .WithMany(s => s.Certificados)
                    .HasForeignKey(e => e.EstudianteID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.RespuestaTest)
                    .WithMany(r => r.Certificados)
                    .HasForeignKey(e => e.RespuestaTestID)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Cita)
                    .WithMany(c => c.Certificados)
                    .HasForeignKey(e => e.CitaID)
                    .OnDelete(DeleteBehavior.SetNull);
            });


            modelBuilder.Entity<Historico>(entity =>
            {
                entity.HasKey(e => e.HistoricoID);
                entity.Property(e => e.TipoActividad).HasMaxLength(100);
                entity.Property(e => e.Descripción).HasColumnType("nvarchar(max)");
                entity.Property(e => e.FechaActividad).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Estudiante)
                    .WithMany(s => s.Historicos)
                    .HasForeignKey(e => e.EstudianteID)
                    .OnDelete(DeleteBehavior.SetNull);
            });


            modelBuilder.Entity<Reporte>(entity =>
            {
                entity.HasKey(e => e.ReporteID);
                entity.Property(e => e.TipoReporte).HasMaxLength(100);
                entity.Property(e => e.TestsMasUtilizados).HasColumnType("nvarchar(max)");
                entity.Property(e => e.NivelesRiesgo).HasColumnType("nvarchar(max)");
                entity.Property(e => e.RutaArchivo).HasMaxLength(500);
                entity.Property(e => e.FechaGeneración).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Administrador)
                .WithMany()
                .HasForeignKey(e => e.AdministradorID)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Administrador)
                    .WithMany()
                    .HasForeignKey(e => e.AdministradorID)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}