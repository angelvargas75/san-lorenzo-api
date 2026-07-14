using Microsoft.EntityFrameworkCore;
using SanLorenzoApi.Dominio;

namespace SanLorenzoApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<GradoSeccion> GradoSecciones { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<DocenteAsignatura> DocenteAsignaturas { get; set; }
        public DbSet<Bimestre> Bimestres { get; set; }
        public DbSet<RegistroNota> RegistrosNotas { get; set; }
        public DbSet<DetalleNota> DetallesNotas { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Comunicado> Comunicados { get; set; }
        public DbSet<Reporte> Reportes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== Usuario 1:1 Alumno =====
            modelBuilder.Entity<Alumno>()
                .HasOne(a => a.Usuario)
                .WithOne(u => u.Alumno)
                .HasForeignKey<Alumno>(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== Usuario 1:1 Docente =====
            modelBuilder.Entity<Docente>()
                .HasOne(d => d.Usuario)
                .WithOne(u => u.Docente)
                .HasForeignKey<Docente>(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== Email de Usuario debe ser único =====
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ===== RegistroNota -> DetalleNota (Cascade) =====
            modelBuilder.Entity<DetalleNota>()
                .HasOne(d => d.RegistroNota)
                .WithMany(r => r.Detalles)
                .HasForeignKey(d => d.RegistroNotaId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== Evitar múltiples cascadas conflictivas (Restrict en la mayoría de FK con múltiples caminos) =====

            // Alumno -> RegistroNota
            modelBuilder.Entity<RegistroNota>()
                .HasOne(r => r.Alumno)
                .WithMany(a => a.RegistrosNotas)
                .HasForeignKey(r => r.AlumnoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Asignatura -> RegistroNota
            modelBuilder.Entity<RegistroNota>()
                .HasOne(r => r.Asignatura)
                .WithMany(a => a.RegistrosNotas)
                .HasForeignKey(r => r.AsignaturaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Bimestre -> RegistroNota
            modelBuilder.Entity<RegistroNota>()
                .HasOne(r => r.Bimestre)
                .WithMany(b => b.RegistrosNotas)
                .HasForeignKey(r => r.BimestreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Alumno -> Asistencia
            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Alumno)
                .WithMany(al => al.Asistencias)
                .HasForeignKey(a => a.AlumnoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Asignatura -> Asistencia
            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Asignatura)
                .WithMany(asig => asig.Asistencias)
                .HasForeignKey(a => a.AsignaturaId)
                .OnDelete(DeleteBehavior.Restrict);

            // DocenteAsignatura -> Docente / Asignatura / GradoSeccion
            modelBuilder.Entity<DocenteAsignatura>()
                .HasOne(da => da.Docente)
                .WithMany(d => d.DocenteAsignaturas)
                .HasForeignKey(da => da.DocenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DocenteAsignatura>()
                .HasOne(da => da.Asignatura)
                .WithMany(a => a.DocenteAsignaturas)
                .HasForeignKey(da => da.AsignaturaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DocenteAsignatura>()
                .HasOne(da => da.GradoSeccion)
                .WithMany(gs => gs.DocenteAsignaturas)
                .HasForeignKey(da => da.GradoSeccionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Horario -> GradoSeccion / Asignatura / Docente
            modelBuilder.Entity<Horario>()
                .HasOne(h => h.GradoSeccion)
                .WithMany(gs => gs.Horarios)
                .HasForeignKey(h => h.GradoSeccionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Asignatura)
                .WithMany()
                .HasForeignKey(h => h.AsignaturaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Docente)
                .WithMany()
                .HasForeignKey(h => h.DocenteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Comunicado -> Usuario (Autor) / GradoSeccion opcional
            modelBuilder.Entity<Comunicado>()
                .HasOne(c => c.Autor)
                .WithMany()
                .HasForeignKey(c => c.AutorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comunicado>()
                .HasOne(c => c.GradoSeccion)
                .WithMany()
                .HasForeignKey(c => c.GradoSeccionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reporte -> Usuario (GeneradoPor)
            modelBuilder.Entity<Reporte>()
                .HasOne(r => r.GeneradoPor)
                .WithMany()
                .HasForeignKey(r => r.GeneradoPorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== Precisión decimal (importante para SQL Server) =====
            modelBuilder.Entity<DetalleNota>()
                .Property(d => d.Valor)
                .HasPrecision(4, 2);   // ej: 20.00

            modelBuilder.Entity<RegistroNota>()
                .Property(r => r.Promedio)
                .HasPrecision(4, 2);
        }
    }
}