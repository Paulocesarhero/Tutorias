// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Sistema_De_Tutorias.Utility;
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

TutoriaManagement tutoriaManagement = new TutoriaManagement();
CreateDB();
InsertData();




static void CreateDB()
{
    using (var context = new TutoriasContext())
    {
        // Creates the database if not exists
        context.Database.EnsureCreated();
    }
}

static bool InsertData()
{
    bool flag = false;
    try
    {
        addPeriodoEscolares();
        addTutorAcademico();
        addReporteDeTutoria();
        addJefeDeCarrera();
        AddCoordinadora();
        addExperienciaEducativa();
        addProblematica1();
        flag = true;
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
    return flag;

}

static bool addPeriodoEscolares()
{
    bool flag;
    TutoriaManagement tutoriaManagement = new TutoriaManagement();
    Periodo_Escolar periodoEscolar = new Periodo_Escolar
    {
        FechaDeFin = new DateTime(2023,02,04),
        FechaDeInicio = new DateTime(2023,06,24),
        FechaDePrimeraTutoria = new DateTime(2023,03,24),
        FechaDeSegundaTutoria = new DateTime(2023,04, 28),
        FechaDeTerceraTutoria = new DateTime(2023,05,31)
            
    };
    try
    {
        tutoriaManagement.AddPeriodoEscolar(periodoEscolar);
        flag = true;
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }

    return flag;
}

static bool addExperienciaEducativa()
{
    
    bool status;
    Experiencia_Educativa experienciaEducativa = new Experiencia_Educativa
    {

        Nombre = "DESARROLLO DE SISTEMAS EN RED",
        Nrc = "78280",
        Catedratico = new Catedratico
        {
            NombreCompleto = "JORGE OCTAVIO OCHARAN HERNADEZ",
        },
        Academia = new Academia("Desarrollos de sistema en red y aplicaciones")
    };
    
    using (TutoriasContext context = new TutoriasContext())
    {
        try
        {
            
            Programa_Educativo programaEducativo = context.ProgramasEducativos.FirstOrDefault();
            experienciaEducativa.ProgramaEducativo = programaEducativo;
            context.ExperienciasEducativas.Add(experienciaEducativa);
            context.SaveChanges();
            status = true;
        }
        catch (DbUpdateException dbUpdateException)
        {
            throw dbUpdateException;
        }
        catch (InvalidOperationException invalidOperationException)
        {
            throw invalidOperationException;
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
    return status;
}

static bool addProblematica()
{
    bool flag;
    TutoriaManagement tutoriaManagement = new TutoriaManagement();
    Problematica problematica = new Problematica
    {
        NumAlumnos = 2,
        Descripcion = "La experiencia educativa se ha tornado repetitiva por el metodo de enseñanza del profesor"
    };
    using (TutoriasContext context = new TutoriasContext())
    {
        try
        {
            Experiencia_Educativa experienciaEducativa = context.ExperienciasEducativas.FirstOrDefault();
            problematica.ExperienciaEducativa = experienciaEducativa;
            flag = true;
        }
        catch (DbUpdateException dbUpdateException)
        {
            throw dbUpdateException;
        }
        catch (InvalidOperationException invalidOperationException)
        {
            throw invalidOperationException;
        }
        catch (Exception exception)
        {
            throw exception;
        }
        flag = true;
    }
    
    return flag;
}


static bool addTutorAcademico()
{
    bool flag;
    TutoriaManagement tutoriaManagement = new TutoriaManagement();
    Tutor_Academico tutor = new Tutor_Academico
    {
        Nombres = "ISRRAEL DAVID",
        Apellidos = "LOPEZ CORZO",
        Usuario = new Usuario
        {
            Password = "OT9nMRgsv2/OEp68hJ1sEIB7LyqxAMlsgCIBK4XSpxRBvNzMaXC9AlhnBLo0HNQNXSyEvAU8KerRN+ZvULZCF8CxeGcZwNpdgSKZSOHtN0T+PWOPovD9poJEYT2fwKN8g5gGYu9ljba0C2hdKrCptw",
            Username = "islopez",
            ProgramaEducativo = new Programa_Educativo
            {
                ProgramaEducativo = "Ingenieria de software"
            },
            TipoUsuario = new TipoUsuario
            {
                Tipo = "Tutor academico"
            }
        }
    };
    try
    {
        tutoriaManagement.AddTutorAcademico(tutor);
        flag = true;
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    return flag;
}

static bool addJefeDeCarrera()
{
    bool flag;
    Usuario usuario = new Usuario
    {
        TipoUsuario = new TipoUsuario
        {
            Tipo = "Jefe de carrera"
        },
        Username = "jocharan",
        Password = Security.HashSHA256("SoyElJefe")
    };
    using (TutoriasContext context = new TutoriasContext())
    {
        try
        {
            Programa_Educativo programa = context.ProgramasEducativos.FirstOrDefault();
            usuario.ProgramaEducativo = programa;
            context.Usuarios.Add(usuario);
            context.SaveChanges();
            flag = true;
        }
        catch (DbUpdateException dbUpdateException)
        {
            throw dbUpdateException;
        }
        catch (InvalidOperationException invalidOperationException)
        {
            throw invalidOperationException;
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    return flag;
}

static bool AddCoordinadora()
{
    bool flag;
    Usuario usuario = new Usuario
    {
        TipoUsuario = new TipoUsuario
        {
            Tipo = "Coordinadora"
        },
        Username = "aarenas",
        Password = Security.HashSHA256("SoyLaCoordinadora")
    };
    using (TutoriasContext context = new TutoriasContext())
    {
        try
        {
            Programa_Educativo programa = context.ProgramasEducativos.FirstOrDefault();
            usuario.ProgramaEducativo = programa;
            context.Usuarios.Add(usuario);
            context.SaveChanges();
            flag = true;
        }
        catch (DbUpdateException dbUpdateException)
        {
            throw dbUpdateException;
        }
        catch (InvalidOperationException invalidOperationException)
        {
            throw invalidOperationException;
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    return flag;
}

static bool addReporteDeTutoria(){
    bool flag;
    TutoriaManagement tutoriaManagement = new TutoriaManagement();
    Reporte_De_Tutoria reporte = new Reporte_De_Tutoria
    {
        Fecha = new DateTime(2023,03,20),
        Comentarios = "Los alumnos en general no se siente satisfechos con su plan de estudios",
        FechaDeCierre = new DateTime(2023,03,28),
        NumDeTutoria = 1,
    };
    using (TutoriasContext context = new TutoriasContext())
    {
        try
        {
            Tutor_Academico tutor = context.TutorAcademico.FirstOrDefault();
            Periodo_Escolar periodoEscolar = context.PeriodosEscolares.FirstOrDefault();
            reporte.TutorAcademico = tutor;
            reporte.PeriodoEscolar = periodoEscolar;
        }
        catch (DbUpdateException dbUpdateException)
        {
            throw dbUpdateException;
        }
        catch (InvalidOperationException invalidOperationException)
        {
            throw invalidOperationException;
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
    
    try
    {
        tutoriaManagement.AddReporteDeTutoria(reporte);
        flag = true;
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    return flag;
}

static bool addProblematica1()
{
    bool flag;
    TutoriaManagement tutoriaManagement = new TutoriaManagement();
    string nrc = "78280";
    Experiencia_Educativa experienciaEducativa = tutoriaManagement.getExperienciaEducativaByNRC(nrc);
    Problematica problematica = new Problematica
    {
        Descripcion = "Los alumnos creen que el profesor no cumple con el conocimiento apropiado para dar la EE",
        NumAlumnos = 2
    };
    
    problematica.ExperienciaEducativa = experienciaEducativa;
    using (TutoriasContext context = new TutoriasContext())
    {
        try
        {
            Reporte_De_Tutoria reporteDeTutoria = context.ReportesDeTutorias.FirstOrDefault();
            problematica.ReporteDeTutoria = reporteDeTutoria;
            flag = true;
        }
        catch (DbUpdateException dbUpdateException)
        {
            throw dbUpdateException;
        }
        catch (InvalidOperationException invalidOperationException)
        {
            throw invalidOperationException;
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
    
    
    try
    {
        tutoriaManagement.AddProblematica(problematica);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    return flag;
}
