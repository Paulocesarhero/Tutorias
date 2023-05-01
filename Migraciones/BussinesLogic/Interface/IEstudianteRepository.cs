using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface IEstudianteRepository
    {
        bool AddEstudiante(Estudiante estudiante);
        bool DeleteEstudiante(Estudiante estudiante);
        bool UpdateEstudiante(Estudiante estudiante);
        List<Estudiante> GetAllEstudiantes();
        List<Estudiante> GetEstudiantesWithOutTutor();

        Estudiante GetEstudianteById(int id);
        List<Estudiante> GetEstudiantesByTutorAcademico(Tutor_Academico tutorAcademico);

    }
}
