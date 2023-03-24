using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
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
                entity.HasKey(e => e.id);
                entity.Property(e => e.nombreCompleto).IsRequired();
            });
            modelBuilder.Entity<Experiencia_Educativa>(entity =>
            {
                entity.HasKey(e => e.nrc);
                entity.HasOne(e => e.catedratico).WithMany(e => e.experienciasEducativas);
                entity.HasMany(e => e.problematicas).WithOne(p => p.experienciaEducativa);
            });
            modelBuilder.Entity<Programa_Educativo>(entity =>
            {
                entity.HasKey(e => e.ProgramaEducativo);
            });
            modelBuilder.Entity<Problematica>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.numAlumnos).IsRequired();
                entity.Property(e => e.descripcion).IsRequired();
                entity.HasOne(e => e.solucion).WithOne(p => p.problematica);
                entity.HasOne(e => e.experienciaEducativa).WithMany(p => p.problematicas);
                entity.HasOne(e => e.reporteDeTutoria).WithMany(p => p.Problematicas);
            });
            modelBuilder.Entity<Solucion>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.titulo).IsRequired();
                entity.Property(e => e.fecha).IsRequired();
                entity.Property(e => e.descripcion).IsRequired();

            }); 
            modelBuilder.Entity<Reporte_De_Tutoria>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.fecha).IsRequired();
                entity.Property(e => e.comentarios).IsRequired();
                entity.Property(e => e.fechaDeCierre).IsRequired();
                entity.HasOne(e => e.tutorAcademico).WithMany(p => p.reportesDeTutorias);
                entity.HasMany(e => e.Problematicas).WithOne(p => p.reporteDeTutoria);

            });
            modelBuilder.Entity<Periodo_Escolar>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.fechaDeInicio).IsRequired();
                entity.Property(e => e.fechaDeFin).IsRequired();


            });
            modelBuilder.Entity<Tutor_Academico>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.nombres).IsRequired();
                entity.Property(e => e.apellidos).IsRequired();
                entity.HasOne(e => e.usuario);
                entity.HasMany(e => e.reportesDeTutorias).WithOne(p => p.tutorAcademico);
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.password).IsRequired();
                entity.Property(e => e.username).IsRequired();
                entity.HasOne(e => e.tipoUsuario);
                entity.HasOne(e => e.ProgramaEducativo);

            });
            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.tipo).IsRequired();
            });
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.nombres).IsRequired();
                entity.Property(e => e.matricula).IsRequired();
                entity.Property(e => e.apellidos).IsRequired();
                entity.HasOne(e => e.tutorAcademico);
                entity.HasMany(e => e.asistencias).WithOne(p => p.estudiante);
            });
            modelBuilder.Entity<Lista_de_Asistencia>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.asiste).IsRequired();
                entity.Property(e => e.horario).IsRequired();
                entity.HasOne(e => e.estudiante).WithMany(p => p.asistencias);
            });
        }
    }

    public class Catedratico
    {
        public Catedratico(int id, string nombres, string apellidos)
        {
            this.id = id;
            this.nombreCompleto = nombres;
         
        }

        public Catedratico()
        {
            this.nombreCompleto = nombreCompleto;
            
        }

        public int id { get; set; }
        public string nombreCompleto { get; set; }
       

        public virtual ICollection<Experiencia_Educativa> experienciasEducativas { get; set; }
    }

    public class Experiencia_Educativa
    {
        public Experiencia_Educativa(string nrc, string nombre)
        {
            this.nrc = nrc;
            this.nombre = nombre;
        }

        public Experiencia_Educativa()
        {
            this.nrc = nrc;
            this.nombre = nombre;
            this.catedratico = catedratico;
            this.programaEducativo = programaEducativo;
        }

        public string nrc { get; set; }
        public string nombre { get; set; }

        public virtual Catedratico catedratico { get; set; }
        public virtual Programa_Educativo programaEducativo { get; set; }
        public virtual ICollection<Problematica> problematicas { get; set; }
    }

    public class Programa_Educativo
    {
        public Programa_Educativo(int id, string programaEducativo)
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
            this.id = id;
            this.numAlumnos = numAlumnos;
            this.descripcion = descripcion;

        }

        

        public Problematica()
        {
            this.id = id;
            this.numAlumnos = numAlumnos;
            this.descripcion = descripcion;
            this.solucion = solucion;
            this.experienciaEducativa = experienciaEducativa;
            this.reporteDeTutoria = reporteDeTutoria;
        }

        public int id { get; set; }
        public int numAlumnos { get; set; }
        public string descripcion { get; set; }
        public virtual Solucion solucion { get; set; }
        public virtual Experiencia_Educativa experienciaEducativa { get; set; }
        
        public virtual Reporte_De_Tutoria reporteDeTutoria { get; set; }
    }

    public class Solucion
    {
        public Solucion(int id, string titulo, DateTime fecha, string descripcion)
        {
            this.id = id;
            this.titulo = titulo;
            this.fecha = fecha;
            this.descripcion = descripcion;
        }

        public int id { get; set; }
        public string titulo { get; set; }
        public DateTime fecha { get; set; }
        public string descripcion { get; set; }
        [ForeignKey("problematicaFk")]
        public virtual Problematica problematica { get; set; }
    }

    public class Reporte_De_Tutoria
    {
        public Reporte_De_Tutoria(int id, DateTime fecha, string comentatios, DateTime fechaDeCierre)
        {
            this.id = id;
            this.fecha = fecha;
            this.comentarios = comentatios;
            this.fechaDeCierre = fechaDeCierre;
        }

        public Reporte_De_Tutoria()
        {
            fecha = fecha;
            comentarios = comentarios;
            fechaDeCierre = fechaDeCierre;
            numDeTutoria = numDeTutoria;
        }

        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string comentarios { get; set; }
        public DateTime fechaDeCierre { get; set; }
        
        public int numDeTutoria { get; set; }
        public virtual Tutor_Academico tutorAcademico { get; set; }
        public virtual Periodo_Escolar periodoEscolar { get; set; }
        public virtual ICollection<Problematica> Problematicas { get; set; }
    }

    public class Periodo_Escolar
    {
        public Periodo_Escolar(int id, DateTime fechaDeInicio, DateTime fechaDeFin, DateTime fechaDePrimeraTutoria, DateTime fechaDeSegundaTutoria, DateTime fechaDeTerceraTutoria)
        {
            this.id = id;
            this.fechaDeInicio = fechaDeInicio;
            this.fechaDeFin = fechaDeFin;
            this.fechaDePrimeraTutoria = fechaDePrimeraTutoria;
            this.fechaDeSegundaTutoria = fechaDeSegundaTutoria;
            this.fechaDeTerceraTutoria = fechaDeTerceraTutoria;
        }

        public Periodo_Escolar()
        {
            this.fechaDeInicio = fechaDeInicio;
            this.fechaDeFin = fechaDeFin;
            this.fechaDePrimeraTutoria = fechaDePrimeraTutoria;
            this.fechaDeSegundaTutoria = fechaDeSegundaTutoria;
            this.fechaDeTerceraTutoria = fechaDeTerceraTutoria;
        }

        public int id { get; set; }
        public DateTime fechaDeInicio { get; set; }
        public DateTime fechaDeFin { get; set; }
        public DateTime fechaDePrimeraTutoria { get; set; }
        public DateTime fechaDeSegundaTutoria { get; set; }
        public DateTime fechaDeTerceraTutoria { get; set; }
    }

    public class Tutor_Academico
    {
        public Tutor_Academico(int id, string nombres, string apellidos)
        {
            this.id = id;
            this.nombres = nombres;
            this.apellidos = apellidos;
        }
        

        public Tutor_Academico()
        {
            nombres = nombres;
            apellidos = apellidos;
            usuario = usuario;
            
        }

        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public virtual Usuario usuario { get; set; }
        public virtual ICollection<Reporte_De_Tutoria> reportesDeTutorias { get; set; }
    }

    public class Usuario
    {
        public Usuario(int id, string password, string username)
        {
            this.id = id;
            this.password = password;
            this.username = username;
        }

        public Usuario()
        {
            id = id;
            password = password;
            username = username;
            tipoUsuario = tipoUsuario;
        }

        public int id { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public virtual TipoUsuario tipoUsuario { get; set; }
        
        public virtual Programa_Educativo ProgramaEducativo { get; set; }
    }

    public class TipoUsuario
    {
        public TipoUsuario(int id, string tipo)
        {
            this.id = id;
            this.tipo = tipo;
        }

        public TipoUsuario()
        {
            tipo = tipo;
        }

        public int id { get; set; }
        public string tipo { get; set; }
    }
    

    public class Estudiante
    {
        public Estudiante(int id, string matricula, string nombres, string apellidos)
        {
            this.id = id;
            this.matricula = matricula;
            this.nombres = nombres;
            this.apellidos = apellidos;
        }

        public int id { get; set; }
        public string matricula { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public virtual Tutor_Academico tutorAcademico { get; set; }

        public virtual ICollection<Lista_de_Asistencia> asistencias { get; set; }
    }

    public class Lista_de_Asistencia
    {
        public Lista_de_Asistencia(int id, bool asiste, DateTime horario)
        {
            this.id = id;
            this.asiste = asiste;
            this.horario = horario;
        }

        public int id { get; set; }
        public bool asiste { get; set; }
        public DateTime horario { get; set; }
        public virtual Estudiante estudiante { get; set; }
    }    
}

