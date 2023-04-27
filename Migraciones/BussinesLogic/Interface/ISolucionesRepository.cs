using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface ISolucionesRepository
    {
        bool AddSolucion(Solucion solucion);
    }
}
