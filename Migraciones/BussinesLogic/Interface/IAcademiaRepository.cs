using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface IAcademiaRepository
    {
        bool AddAcademia(Academia academia);
        bool DeleteAcademia(Academia academia);
        List<Academia> getAllAcademias();
        public bool UpdateAcademia(Academia academia);
    }
}
