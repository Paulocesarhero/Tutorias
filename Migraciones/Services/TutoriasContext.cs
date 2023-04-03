using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Tutorias.Service.DatabaseContext
{
    public class TutoriasContext : DbContext
    {
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
        public DbSet<Lista_de_Asistencia> ListaDeAsistencias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;" +
                                     
                                    "database=database;" +
                                    "user=user;" +
                                    "password=password");
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
            });
            modelBuilder.Entity<Programa_Educativo>(entity =>
            {
                entity.HasKey(e => e.ProgramaEducativo);
            });
            modelBuilder.Entity<Academia>(entity =>
            {
                entity.HasKey(e => e.NombreAcademia);
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
                entity.Property(e => e.FechaDeCierre).IsRequired();
                entity.HasOne(e => e.TutorAcademico).WithMany(p => p.ReportesDeTutorias);
                entity.HasMany(e => e.Problematicas).WithOne(p => p.ReporteDeTutoria);

            });
            modelBuilder.Entity<Periodo_Escolar>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FechaDeInicio).IsRequired();
                entity.Property(e => e.FechaDeFin).IsRequired();


            });
            modelBuilder.Entity<Tutor_Academico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombres).IsRequired();
                entity.Property(e => e.Apellidos).IsRequired();
                entity.HasOne(e => e.Usuario);
                entity.HasMany(e => e.ReportesDeTutorias).WithOne(p => p.TutorAcademico);
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Username).IsRequired();
                entity.HasOne(e => e.TipoUsuario);
                entity.HasOne(e => e.ProgramaEducativo);

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
                entity.HasOne(e => e.TutorAcademico);
                entity.HasMany(e => e.Asistencias).WithOne(p => p.Estudiante);
            });
            modelBuilder.Entity<Lista_de_Asistencia>(entity =>
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
        }

        public string ProgramaEducativo { get; set; }
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

        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        [ForeignKey("problematicaFk")]
        public virtual Problematica Problematica { get; set; }
    }

    public class Reporte_De_Tutoria
    {
        public Reporte_De_Tutoria(int id, DateTime fecha, string comentatios, DateTime fechaDeCierre)
        {
            this.Id = id;
            this.Fecha = fecha;
            this.Comentarios = comentatios;
            this.FechaDeCierre = fechaDeCierre;
        }

        public Reporte_De_Tutoria()
        {
            Fecha = Fecha;
            Comentarios = Comentarios;
            FechaDeCierre = FechaDeCierre;
            NumDeTutoria = NumDeTutoria;
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaDeCierre { get; set; }
        
        public int NumDeTutoria { get; set; }
        public virtual Tutor_Academico TutorAcademico { get; set; }
        public virtual Periodo_Escolar PeriodoEscolar { get; set; }
        public virtual ICollection<Problematica> Problematicas { get; set; }
    }

    public class Periodo_Escolar
    {
        public Periodo_Escolar(int id, DateTime fechaDeInicio, DateTime fechaDeFin, DateTime fechaDePrimeraTutoria, DateTime fechaDeSegundaTutoria, DateTime fechaDeTerceraTutoria)
        {
            this.Id = id;
            this.FechaDeInicio = fechaDeInicio;
            this.FechaDeFin = fechaDeFin;
            this.FechaDePrimeraTutoria = fechaDePrimeraTutoria;
            this.FechaDeSegundaTutoria = fechaDeSegundaTutoria;
            this.FechaDeTerceraTutoria = fechaDeTerceraTutoria;
        }

        public Periodo_Escolar()
        {
            this.FechaDeInicio = FechaDeInicio;
            this.FechaDeFin = FechaDeFin;
            this.FechaDePrimeraTutoria = FechaDePrimeraTutoria;
            this.FechaDeSegundaTutoria = FechaDeSegundaTutoria;
            this.FechaDeTerceraTutoria = FechaDeTerceraTutoria;
        }

        public int Id { get; set; }
        public DateTime FechaDeInicio { get; set; }
        public DateTime FechaDeFin { get; set; }
        public DateTime FechaDePrimeraTutoria { get; set; }
        public DateTime FechaDeSegundaTutoria { get; set; }
        public DateTime FechaDeTerceraTutoria { get; set; }
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

        public int Id { get; set; }
        public string Matricula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public virtual Tutor_Academico TutorAcademico { get; set; }

        public virtual ICollection<Lista_de_Asistencia> Asistencias { get; set; }
    }

    public class Lista_de_Asistencia
    {
        public Lista_de_Asistencia(int id, bool asiste, DateTime horario)
        {
            this.Id = id;
            this.Asiste = asiste;
            this.Horario = horario;
        }

        public int Id { get; set; }
        public bool Asiste { get; set; }
        public DateTime Horario { get; set; }
        public virtual Estudiante Estudiante { get; set; }
    }    
}

