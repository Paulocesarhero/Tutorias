using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BussinesLogic.Interface;
using Microsoft.EntityFrameworkCore;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.EntityRepository
{
    public class PeriodoEscolarRepository : IPeriodosEscolaresRepository
    {
        private readonly DbContext _context;

        public PeriodoEscolarRepository(DbContext context)
        {
            _context = context;
        }

        public bool AddPeriodoEscolar(Periodo_Escolar periodoEscolar)
        {
            try
            {
                _context.Set<Periodo_Escolar>().Add(periodoEscolar);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al agregar periodo escolar",e);
            }
        }

        public bool UpdatePeriodoEscolar(Periodo_Escolar periodoEscolar)
        {
            try
            {
                Periodo_Escolar exist = _context.Set<Periodo_Escolar>().FirstOrDefault(x => x.Id == periodoEscolar.Id);
                if (exist == null)
                {
                    throw new Exception("Error no se encontro el periodo escolar");
                }
                _context.Set<Periodo_Escolar>().Update(periodoEscolar);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al actualizar periodo escolar", e);
            }
        }

        public bool DeletePeriodoEscolar(Periodo_Escolar periodoEscolar)
        {
            try
            {
                _context.Set<Periodo_Escolar>().Remove(periodoEscolar);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al eliminar el periodo escolar", e);
            }
        }

        public List<Periodo_Escolar> GetAllPeriodosEscolar()
        {
            try
            {
                return _context.Set<Periodo_Escolar>().ToList();
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al obtener los periodos escolares", e);
            }
        }
    }
}
