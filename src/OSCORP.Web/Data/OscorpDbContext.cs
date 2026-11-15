using Microsoft.EntityFrameworkCore;
using OSCORP.Web.Models;

namespace OSCORP.Web.Data;

public class OscorpDbContext(DbContextOptions<OscorpDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Especialidad> Especialidades => Set<Especialidad>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Doctor> Doctores => Set<Doctor>();
    public DbSet<Horario> Horarios => Set<Horario>();
    public DbSet<CitaMedica> CitasMedicas => Set<CitaMedica>();
    public DbSet<Comprobante> Comprobantes => Set<Comprobante>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("usuarios");
            e.HasKey(x => x.IdUsuario);
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            e.Property(x => x.Correo).HasColumnName("correo").HasMaxLength(100).IsRequired();
            e.Property(x => x.Contrasena).HasColumnName("contrasena").HasMaxLength(255).IsRequired();
            e.Property(x => x.TipoUsuario).HasColumnName("tipo_usuario").HasMaxLength(20).IsRequired();
            e.HasIndex(x => x.Correo).IsUnique();
        });
        modelBuilder.Entity<Especialidad>(e =>
        {
            e.ToTable("especialidades");
            e.HasKey(x => x.IdEspecialidad);
            e.Property(x => x.IdEspecialidad).HasColumnName("id_especialidad");
            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            e.Property(x => x.Descripcion).HasColumnName("descripcion").HasMaxLength(255);
        });
        modelBuilder.Entity<Paciente>(e =>
        {
            e.ToTable("pacientes");
            e.HasKey(x => x.IdPaciente);
            e.Property(x => x.IdPaciente).HasColumnName("id_paciente");
            e.HasOne(x => x.Usuario).WithOne().HasForeignKey<Paciente>(x => x.IdPaciente);
        });
        modelBuilder.Entity<Doctor>(e =>
        {
            e.ToTable("doctores");
            e.HasKey(x => x.IdDoctor);
            e.Property(x => x.IdDoctor).HasColumnName("id_doctor");
            e.Property(x => x.IdEspecialidad).HasColumnName("id_especialidad");
            e.HasOne(x => x.Usuario).WithOne().HasForeignKey<Doctor>(x => x.IdDoctor);
            e.HasOne(x => x.Especialidad).WithMany().HasForeignKey(x => x.IdEspecialidad);
        });
        modelBuilder.Entity<Horario>(e =>
        {
            e.ToTable("horarios");
            e.HasKey(x => x.IdHorario);
            e.Property(x => x.IdHorario).HasColumnName("id_horario");
            e.Property(x => x.IdDoctor).HasColumnName("id_doctor");
            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("date");
            e.Property(x => x.HoraInicio).HasColumnName("hora_inicio");
            e.Property(x => x.HoraFin).HasColumnName("hora_fin");
            e.Property(x => x.Estado).HasColumnName("estado").HasMaxLength(20);
            e.HasOne(x => x.Doctor).WithMany().HasForeignKey(x => x.IdDoctor);
        });
        modelBuilder.Entity<CitaMedica>(e =>
        {
            e.ToTable("citas_medicas");
            e.HasKey(x => x.IdCita);
            e.Property(x => x.IdCita).HasColumnName("id_cita");
            e.Property(x => x.IdPaciente).HasColumnName("id_paciente");
            e.Property(x => x.IdHorario).HasColumnName("id_horario");
            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("date");
            e.Property(x => x.Hora).HasColumnName("hora");
            e.Property(x => x.MotivoConsulta).HasColumnName("motivo_consulta").HasMaxLength(255);
            e.Property(x => x.Estado).HasColumnName("estado").HasMaxLength(20);
            e.HasIndex(x => x.IdHorario).IsUnique();
            e.HasOne(x => x.Paciente).WithMany().HasForeignKey(x => x.IdPaciente);
            e.HasOne(x => x.Horario).WithMany().HasForeignKey(x => x.IdHorario);
        });
        modelBuilder.Entity<Comprobante>(e =>
        {
            e.ToTable("comprobantes");
            e.HasKey(x => x.IdComprobante);
            e.Property(x => x.IdComprobante).HasColumnName("id_comprobante");
            e.Property(x => x.IdCita).HasColumnName("id_cita");
            e.Property(x => x.FechaEmision).HasColumnName("fecha_emision");
            e.Property(x => x.Tipo).HasColumnName("tipo").HasMaxLength(50);
            e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(10, 2);
            e.HasIndex(x => x.IdCita).IsUnique();
        });
    }
}
