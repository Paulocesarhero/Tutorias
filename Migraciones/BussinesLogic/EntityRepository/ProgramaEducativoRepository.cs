using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BussinesLogic.Interface;
using Microsoft.EntityFrameworkCore;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.EntityRepository
{
    public class ProgramaEducativoRepository : IProgramasEducativosRepository
    {
        private readonly DbContext _context;

        public ProgramaEducativoRepository(DbContext context)
        {
            _context = context;
        }

        public List<Programa_Educativo> GetProgramasEducativos()
        {
            try
            {
                return _context.Set<Programa_Educativo>().ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener todos los programas educativos", e);
            }
        }

        public bool AddProgramaEducativo(Programa_Educativo programaEducativo)
        {
            try
            {
                Programa_Educativo exist =
                    _context.Set<Programa_Educativo>().FirstOrDefault(
                        prog => prog.ProgramaEducativo == programaEducativo.ProgramaEducativo);
                if (exist != null)
                {
                    return true;
                }

                _context.Set<Programa_Educativo>().Add(programaEducativo);
                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al agregar programa educativo", e);
            }
        }

        public bool DeleteProgramaEducativo(Programa_Educativo programaEducativo)
        {
            try
            {
                Programa_Educativo exist =
                    _context.Set<Programa_Educativo>().FirstOrDefault(
                        prog => prog.ProgramaEducativo == programaEducativo.ProgramaEducativo);
                if (exist == null)
                {
                    throw new Exception("Programa educativo not found");
                }
                _context.Set<Programa_Educativo>().Remove(exist);
                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al eliminar programa educativo", e);
            }
        }
    }
}
