using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface IReportesDeTutoriasRepository
    {
        bool AddReporteDeTutoria(Reporte_De_Tutoria reporteDeTutoria);

        Reporte_De_Tutoria GetReporteDeTutoria(Tutor_Academico tutorAcademico, Fecha_De_Tutoria fechaDeTutoria);
    }
}
