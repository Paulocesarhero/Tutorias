using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface IAsistenciasRepository
    {
        int GetTotalDeAsistencias(Periodo_Escolar periodoEscolar, int numDeSesion);
        bool AddAsistencia(Asistencia asistencia);
        bool DeleteAsistencia(Asistencia asistencia);

        List<Asistencia> GetAllAsistencias();
    }
}
