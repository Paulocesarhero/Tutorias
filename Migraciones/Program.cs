// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Sistema_De_Tutorias.Utility;
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

//CreateDB();
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
    var flag = false;
    try
    {
        addPeriodoEscolares();
        addTutorAcademico();
        addReporteDeTutoria();
        addJefeDeCarrera();
        AddCoordinadora();
        addExperienciaEducativa();
        addProblematica1();
        addEstudiante();

        

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
    var tutoriaManagement = new TutoriaManagement();
    var periodoEscolar = new Periodo_Escolar
    {
        FechaDeFin = new DateTime(2023, 02, 04),
        FechaDeInicio = new DateTime(2023, 06, 24),
        FechasDeTutorias = new List<Fecha_De_Tutoria>
        {
            new()
            {
                NumDeTutoria = 1,
                FechaDeCierre = new DateTime(2023, 03, 24)
            },
            new()
            {
                NumDeTutoria = 2,
                FechaDeCierre = new DateTime(2023, 04, 28)
            },
            new()
            {
                NumDeTutoria = 3,
                FechaDeCierre = new DateTime(2023, 05, 31)
            }
        }
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
    var experienciaEducativa = new Experiencia_Educativa
    {
        Nombre = "DESARROLLO DE SISTEMAS EN RED",
        Nrc = "78280",
        Catedratico = new Catedratico
        {
            NombreCompleto = "JORGE OCTAVIO OCHARAN HERNADEZ"
        },
        Academia = new Academia("Desarrollos de sistema en red y aplicaciones")
    };

    using (var context = new TutoriasContext())
    {
        try
        {
            var programaEducativo = context.ProgramasEducativos.FirstOrDefault();
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
    var tutoriaManagement = new TutoriaManagement();
    var problematica = new Problematica
    {
        NumAlumnos = 2,
        Descripcion = "La experiencia educativa se ha tornado repetitiva por el metodo de enseñanza del profesor"
    };
    using (var context = new TutoriasContext())
    {
        try
        {
            var experienciaEducativa = context.ExperienciasEducativas.FirstOrDefault();
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
    var tutoriaManagement = new TutoriaManagement();
    var tutor = new Tutor_Academico
    {
        Nombres = "ISRRAEL DAVID",
        Apellidos = "LOPEZ CORZO",
        Usuario = new Usuario
        {
            Password =
                "OT9nMRgsv2/OEp68hJ1sEIB7LyqxAMlsgCIBK4XSpxRBvNzMaXC9AlhnBLo0HNQNXSyEvAU8KerRN+ZvULZCF8CxeGcZwNpdgSKZSOHtN0T+PWOPovD9poJEYT2fwKN8g5gGYu9ljba0C2hdKrCptw",
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
    var usuario = new Usuario
    {
        TipoUsuario = new TipoUsuario
        {
            Tipo = "Jefe de carrera"
        },
        Username = "jocharan",
        Password = Security.HashSHA256("SoyElJefe")
    };
    using (var context = new TutoriasContext())
    {
        try
        {
            var programa = context.ProgramasEducativos.FirstOrDefault();
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
    var usuario = new Usuario
    {
        TipoUsuario = new TipoUsuario
        {
            Tipo = "Coordinadora"
        },
        Username = "aarenas",
        Password = Security.HashSHA256("SoyLaCoordinadora")
    };
    using (var context = new TutoriasContext())
    {
        try
        {
            var programa = context.ProgramasEducativos.FirstOrDefault();
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

static bool addReporteDeTutoria()
{
    bool flag;
    var tutoriaManagement = new TutoriaManagement();
    var reporte = new Reporte_De_Tutoria
    {
        Fecha = new DateTime(2023, 03, 20),
        Comentarios = "Los alumnos en general no se siente satisfechos con su plan de estudios",
    };
    using (var context = new TutoriasContext())
    {
        try
        {
            var tutor = context.TutorAcademico.FirstOrDefault();
            var fechaDeTutoria = context.FechasDeTutorias.FirstOrDefault();
            reporte.TutorAcademico = tutor;
            reporte.FechaDeTutoria = fechaDeTutoria;
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
    var tutoriaManagement = new TutoriaManagement();
    var nrc = "78280";
    var experienciaEducativa = tutoriaManagement.getExperienciaEducativaByNRC(nrc);
    var problematica = new Problematica
    {
        Descripcion = "Los alumnos creen que el profesor no cumple con el conocimiento apropiado para dar la EE",
        NumAlumnos = 2
    };

    problematica.ExperienciaEducativa = experienciaEducativa;
    using (var context = new TutoriasContext())
    {
        try
        {
            var reporteDeTutoria = context.ReportesDeTutorias.FirstOrDefault();
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

static void addEstudiante()
{
    TutoriaManagement tutoriaManagement = new TutoriaManagement();
    Estudiante estudiante = new Estudiante
    {
        Nombres = "Paulo Cesar",
        Apellidos = "Hernandez Rosado",
        Matricula = "S20020854"

    };
    tutoriaManagement.AddEstudiante(estudiante);
}