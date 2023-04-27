using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface IFechaDeTutoria
    {
        Fecha_De_Tutoria GetFechaDeTutoriaActual(DateTime fechaDeHoy);

        bool AddFechaDeTutoria(Fecha_De_Tutoria fechaDeTutoria);

        bool DeleteFechaDeTutoria(Fecha_De_Tutoria fechaDeTutoria);
    }
}
