using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BussinesLogic.Interface;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.EntityRepository
{
    public class FechaDeTutoriaRepository : IFechaDeTutoria
    {
        private readonly DbContext _context;

        public FechaDeTutoriaRepository(DbContext context)
        {
            _context = context;
        }


        public Fecha_De_Tutoria GetFechaDeTutoriaActual(DateTime fechaDeHoy)
        {
            try
            {
                return _context.Set<Fecha_De_Tutoria>().FirstOrDefault(x => x.FechaDeCierre > fechaDeHoy);
            }
            catch (DbException e)
            {
                throw new Exception("Error al encontrar fechas de tutorias", e);
            }
        }

        public bool AddFechaDeTutoria(Fecha_De_Tutoria fechaDeTutoria)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFechaDeTutoria(Fecha_De_Tutoria fechaDeTutoria)
        {
            throw new NotImplementedException();
        }
    }
}
