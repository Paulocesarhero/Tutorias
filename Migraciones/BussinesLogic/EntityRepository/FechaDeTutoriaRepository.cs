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
            try
            {
                if (fechaDeTutoria.Id == 0)
                {
                    _context.Set<Periodo_Escolar>().Attach(fechaDeTutoria.PeriodoEscolar);
                    _context.Set<Fecha_De_Tutoria>().Add(fechaDeTutoria);
                }
                else
                {
                    // Si el objeto tiene un ID, es una entidad existente, por lo que la actualizamos.
                    _context.Set<Fecha_De_Tutoria>().Update(fechaDeTutoria);
                }

                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al agregar las tutorias academicas", e);
            }
        }

        public bool DeleteFechaDeTutoria(Fecha_De_Tutoria fechaDeTutoria)
        {
            throw new NotImplementedException();
        }
    }
}
