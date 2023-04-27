using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface ICatedraticoRepository
    {
        Catedratico GetByName(string name);
        bool AddCatedratico(Catedratico catedratico);
        bool UpdateCatedratico(Catedratico catedratico);
        bool DeleteCatedratico(Catedratico catedratico);

        public List<Catedratico> GetAllCatedraticos();
    }
}
