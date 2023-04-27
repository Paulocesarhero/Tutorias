using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface ITutorAcademicoRepository
    {
        bool AddTutorAcademico(Tutor_Academico tutorAcademico);
        bool DeleteTutorAcademico(Tutor_Academico tutorAcademico);
        bool UpdateTutorAcademico(Tutor_Academico tutorAcademico);
        Tutor_Academico GetTutorAcademico(int id);  

    }
}
