using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Pomelo.EntityFrameworkCore.MySql;
namespace Tutorias.Service.DatabaseContext
{
    public class TutoriasContext : DbContext
    {
        static readonly string connectionString = "";
        public DbSet<Catedratico> Catedraticos { get; set; }
        public DbSet<Experiencia_Educativa> ExperienciasEducativas { get; set; }
        public DbSet<Programa_Educativo> ProgramasEducativos { get; set; }
        public DbSet<Problematica> Problematicas { get; set; }
        public DbSet<Solucion> Soluciones { get; set; }
        public DbSet<Reporte_De_Tutoria> ReportesDeTutorias { get; set; }
        public DbSet<Periodo_Escolar> PeriodosEscolares { get; set; }
        public DbSet<Tutor_Academico> TutorAcademico { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoUsuario> TiposUsuarios { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }

        public DbSet<Fecha_De_Tutoria> FechasDeTutorias { get; set; }

        public DbSet<Academia> Academias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catedratico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NombreCompleto).IsRequired();
            });
            modelBuilder.Entity<Experiencia_Educativa>(entity =>
            {
                entity.HasKey(e => e.Nrc);
                entity.HasOne(e => e.Catedratico).WithMany(e => e.ExperienciasEducativas);
                entity.HasMany(e => e.Problematicas).WithOne(p => p.ExperienciaEducativa);
                entity.HasOne(e => e.Academia).WithMany(a => a.ExperienciaEducativas );
                entity.HasOne(e => e.ProgramaEducativo).WithMany(p => p.ExperienciaEducativas);
            });
            modelBuilder.Entity<Programa_Educativo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.ExperienciaEducativas).WithOne(p => p.ProgramaEducativo);
                entity.HasMany(u => u.Usuarios).WithOne(u => u.ProgramaEducativo);
            });
            modelBuilder.Entity<Academia>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(e => e.ExperienciaEducativas).WithOne(a => a.Academia);
            });
            modelBuilder.Entity<Problematica>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NumAlumnos).IsRequired();
                entity.Property(e => e.Descripcion).IsRequired();
                entity.HasOne(e => e.Solucion).WithOne(p => p.Problematica);
                entity.HasOne(e => e.ExperienciaEducativa).WithMany(p => p.Problematicas);
                entity.HasOne(e => e.ReporteDeTutoria).WithMany(p => p.Problematicas);
            });
            modelBuilder.Entity<Solucion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired();
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Descripcion).IsRequired();

            }); 
            modelBuilder.Entity<Reporte_De_Tutoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Comentarios).IsRequired();
                entity.HasOne(e => e.TutorAcademico).WithMany(p => p.ReportesDeTutorias);
                entity.HasMany(e => e.Problematicas).WithOne(p => p.ReporteDeTutoria);
                entity.HasOne(r => r.FechaDeTutoria).WithMany(f => f.ReportesDeTutorias);

            });
            modelBuilder.Entity<Fecha_De_Tutoria>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.NumDeTutoria).IsRequired();
                    entity.HasOne(e => e.PeriodoEscolar).WithMany(p => p.FechasDeTutorias);
                    entity.HasMany(f => f.Asistencias).WithOne(a => a.FechaDeTutoria);
                    entity.HasMany(f => f.ReportesDeTutorias).WithOne(r => r.FechaDeTutoria);
                }
            );
            modelBuilder.Entity<Periodo_Escolar>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FechaDeInicio).IsRequired();
                entity.Property(e => e.FechaDeFin).IsRequired();
                entity.HasMany(p => p.FechasDeTutorias).WithOne(f => f.PeriodoEscolar);
            });
            modelBuilder.Entity<Tutor_Academico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombres).IsRequired();
                entity.Property(e => e.Apellidos).IsRequired();
                entity.HasOne(e => e.Usuario);
                entity.HasMany(e => e.ReportesDeTutorias).WithOne(p => p.TutorAcademico);
                entity.HasMany(tutor => tutor.Estudiantes).WithOne(estudiante => estudiante.TutorAcademico);
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Username).IsRequired();
                entity.HasOne(e => e.TipoUsuario);
                entity.HasOne(e => e.ProgramaEducativo).WithMany(p => p.Usuarios);

            });
            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Tipo).IsRequired();
            });
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombres).IsRequired();
                entity.Property(e => e.Matricula).IsRequired();
                entity.Property(e => e.Apellidos).IsRequired();
                entity.HasOne(e => e.TutorAcademico).WithMany(tutor => tutor.Estudiantes).HasForeignKey(e => e.IdTutorAcademico);
                entity.HasMany(e => e.Asistencias).WithOne(p => p.Estudiante);
            });
            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Asiste).IsRequired();
                entity.Property(e => e.Horario).IsRequired();
                entity.HasOne(e => e.Estudiante).WithMany(p => p.Asistencias);
            });
        }
    }

    public class Catedratico
    {
        public Catedratico(int id, string nombres, string apellidos)
        {
            this.Id = id;
            this.NombreCompleto = nombres;
         
        }

        public Catedratico()
        {
            this.NombreCompleto = NombreCompleto;
            
        }

        public int Id { get; set; }
        public string NombreCompleto { get; set; }
       

        public virtual ICollection<Experiencia_Educativa> ExperienciasEducativas { get; set; }
    }

    public class Experiencia_Educativa
    {
        public Experiencia_Educativa(string nrc, string nombre)
        {
            this.Nrc = nrc;
            this.Nombre = nombre;
        }

        public Experiencia_Educativa()
        {
            this.Nrc = Nrc;
            this.Nombre = Nombre;
            this.Catedratico = Catedratico;
            this.ProgramaEducativo = ProgramaEducativo;
        }

        public string Nrc { get; set; }
        public string Nombre { get; set; }

        public virtual Catedratico Catedratico { get; set; }
        public virtual Programa_Educativo ProgramaEducativo { get; set; }
        public virtual ICollection<Problematica> Problematicas { get; set; }

        public virtual Academia Academia { get; set; }
    }

    public class Academia
    {
        public Academia(string nombreAcademia)
        {
            NombreAcademia = nombreAcademia;
        }

        public Academia()
        {
            NombreAcademia = NombreAcademia;
        }
        public int  Id { get; set; }
        public string NombreAcademia { get; set; }
        public virtual ICollection<Experiencia_Educativa> ExperienciaEducativas { get; set; }


    }

    public class Programa_Educativo
    {
        public Programa_Educativo(string programaEducativo)
        {
            this.ProgramaEducativo = programaEducativo;
        }

        public Programa_Educativo()
        {
            this.ProgramaEducativo = ProgramaEducativo;
            this.Id = Id;
        }
        public int? Id {get; set; }

        public string ProgramaEducativo { get; set; }

        public virtual ICollection<Experiencia_Educativa> ExperienciaEducativas { get; set; }

        public virtual ICollection<Usuario> Usuarios {get; set; }
    }

    public class Problematica
    {

        public Problematica(int id, int numAlumnos, string descripcion)
        {
            this.Id = id;
            this.NumAlumnos = numAlumnos;
            this.Descripcion = descripcion;

        }

        

        public Problematica()
        {
            this.Id = Id;
            this.NumAlumnos = NumAlumnos;
            this.Descripcion = Descripcion;
            this.Solucion = Solucion;
            this.ExperienciaEducativa = ExperienciaEducativa;
            this.ReporteDeTutoria = ReporteDeTutoria;
        }

        public int Id { get; set; }
        public int NumAlumnos { get; set; }
        public string Descripcion { get; set; }
        public virtual Solucion Solucion { get; set; }
        public virtual Experiencia_Educativa ExperienciaEducativa { get; set; }
        
        public virtual Reporte_De_Tutoria ReporteDeTutoria { get; set; }
    }

    public class Solucion
    {
        public Solucion(int id, string titulo, DateTime fecha, string descripcion)
        {
            this.Id = id;
            this.Titulo = titulo;
            this.Fecha = fecha;
            this.Descripcion = descripcion;
        }

        public Solucion()
        {
            Id = Id;
            Titulo = Titulo;
            Fecha = Fecha;
            Descripcion = Descripcion;
            Problematica = Problematica;
        }


        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        [ForeignKey("problematicaFk")]
        public virtual Problematica Problematica { get; set; }
    }

    public class Reporte_De_Tutoria
    {
        public Reporte_De_Tutoria(int id, DateTime fecha, string comentatios)
        {
            this.Id = id;
            this.Fecha = fecha;
            this.Comentarios = comentatios;
        }

        public Reporte_De_Tutoria()
        {
            Fecha = Fecha;
            Comentarios = Comentarios;
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentarios { get; set; }
        
        
        public virtual Tutor_Academico TutorAcademico { get; set; }
        public virtual ICollection<Problematica> Problematicas { get; set; }
        public virtual Fecha_De_Tutoria FechaDeTutoria { get; set; }
    }

    public class Fecha_De_Tutoria
    {
        public int Id { get; set; }
        public DateTime FechaDeCierre { get; set; }
        public int NumDeTutoria { get; set; }
        public virtual Periodo_Escolar PeriodoEscolar { get; set; }

        public virtual ICollection<Asistencia> Asistencias { get; set; }

        public  virtual ICollection<Reporte_De_Tutoria> ReportesDeTutorias { get; set; }


    }

    public class Periodo_Escolar
    {
        public Periodo_Escolar(int id, DateTime fechaDeInicio, DateTime fechaDeFin)
        {
            this.Id = id;
            this.FechaDeInicio = fechaDeInicio;
            this.FechaDeFin = fechaDeFin;
        }

        public Periodo_Escolar()
        {
            this.FechaDeInicio = FechaDeInicio;
            this.FechaDeFin = FechaDeFin;
        }

        public int Id { get; set; }
        public DateTime FechaDeInicio { get; set; }
        public DateTime FechaDeFin { get; set; }

        public ICollection<Fecha_De_Tutoria> FechasDeTutorias { get; set; }

    }

    public class Tutor_Academico
    {
        public Tutor_Academico(int id, string nombres, string apellidos)
        {
            this.Id = id;
            this.Nombres = nombres;
            this.Apellidos = apellidos;
        }
        

        public Tutor_Academico()
        {
            Nombres = Nombres;
            Apellidos = Apellidos;
            Usuario = Usuario;
            
        }

        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Reporte_De_Tutoria> ReportesDeTutorias { get; set; }

        public virtual ICollection<Estudiante> Estudiantes { get; set; }
    }

    public class Usuario
    {
        public Usuario(int id, string password, string username)
        {
            this.Id = id;
            this.Password = password;
            this.Username = username;
        }

        public Usuario()
        {
            Id = Id;
            Password = Password;
            Username = Username;
            TipoUsuario = TipoUsuario;
        }

        public int Id { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
        
        public virtual Programa_Educativo ProgramaEducativo { get; set; }
    }

    public class TipoUsuario
    {
        public TipoUsuario(int id, string tipo)
        {
            this.Id = id;
            this.Tipo = tipo;
        }

        public TipoUsuario()
        {
            Tipo = Tipo;
        }

        public int Id { get; set; }
        public string Tipo { get; set; }
    }
    

    public class Estudiante
    {
        public Estudiante(int id, string matricula, string nombres, string apellidos)
        {
            this.Id = id;
            this.Matricula = matricula;
            this.Nombres = nombres;
            this.Apellidos = apellidos;
        }

        public Estudiante()
        {
            Matricula = Matricula;
            Nombres = Nombres;
            Apellidos = Apellidos;
        }

        public int Id { get; set; }
        public string Matricula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public int? IdTutorAcademico { get; set; }
        public virtual Tutor_Academico TutorAcademico { get; set; }

        public virtual ICollection<Asistencia> Asistencias { get; set; }
    }

    public class Asistencia
    {
        public Asistencia(int id, bool asiste, DateTime horario)
        {
            this.Id = id;
            this.Asiste = asiste;
            this.Horario = horario;
        }

        

        public Asistencia()
        {
            Asiste = Asiste;
            Horario = Horario;
            Estudiante = Estudiante;
            FechaDeTutoria = FechaDeTutoria;
        }

        public int Id { get; set; }
        public bool Asiste { get; set; }
        public DateTime Horario { get; set; }
        public virtual Estudiante Estudiante { get; set; }
        public virtual Fecha_De_Tutoria FechaDeTutoria { get; set; }
    }    
}

