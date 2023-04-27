using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    public interface IProgramasEducativosRepository
    {
        List<Programa_Educativo> GetProgramasEducativos();
        bool AddProgramaEducativo(Programa_Educativo programaEducativo);
        bool DeleteProgramaEducativo(Programa_Educativo programaEducativo);
    }
}
