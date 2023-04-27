using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface IExperienciasEducativasRepository
    {
        Experiencia_Educativa GetByNrc(string nrc);
        bool AddExperienciaEducativa(Experiencia_Educativa experienciaEducativa);
        bool UpdateExperienciaEducativa(Experiencia_Educativa experienciaEducativa);
        bool DeleteExperienciaEducativa(Experiencia_Educativa experienciaEducativa);

        public List<Experiencia_Educativa> GetExperienciasEducativas();



    }
}
