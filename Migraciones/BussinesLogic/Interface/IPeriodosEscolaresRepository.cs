using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface IPeriodosEscolaresRepository
    {
        bool AddPeriodoEscolar(Periodo_Escolar periodoEscolar);
        bool UpdatePeriodoEscolar(Periodo_Escolar periodoEscolar);
        bool DeletePeriodoEscolar(Periodo_Escolar periodoEscolar);
        List<Periodo_Escolar> GetAllPeriodosEscolar();

    }
}
