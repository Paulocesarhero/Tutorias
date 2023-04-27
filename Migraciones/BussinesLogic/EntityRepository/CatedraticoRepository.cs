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
    public class CatedraticoRepository: ICatedraticoRepository
    {
        private readonly DbContext _context;
        public CatedraticoRepository(DbContext context)
        {
            _context = context;
        }


        public Catedratico GetByName(string nombreCompleto)
        {
            try
            {
               return _context.Set<Catedratico>().Where(catedratico => catedratico.NombreCompleto == nombreCompleto)
                   .Include(catedratico => catedratico.ExperienciasEducativas).FirstOrDefault();
            }
            catch (DbException e)
            {
                throw new Exception("Error al buscar catedratico", e);
            }
        }

        public bool AddCatedratico(Catedratico catedratico)
        {
            try
            {
                Catedratico exist =
                    _context.Set<Catedratico>().FirstOrDefault(
                        cat => cat.NombreCompleto == catedratico.NombreCompleto);
                if (exist != null)
                {
                    return true;
                }

                _context.Set<Catedratico>().Add(catedratico);
                return  _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al agregar catedratico", e);
            }
        }

        public bool UpdateCatedratico(Catedratico catedratico)
        {
            try
            {
                Catedratico existingCatedratico = _context.Set<Catedratico>().FirstOrDefault(cat => cat.Id == catedratico.Id);
                if (existingCatedratico == null)
                {
                    throw new Exception("Catedratico not found");
                }
                _context.Set<Catedratico>().Update(catedratico);
                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al actualizar catedratico", e);
            }
        }

        public bool DeleteCatedratico(Catedratico catedratico)
        {
            try
            {
                Catedratico existingCatedratico = _context.Set<Catedratico>().FirstOrDefault(cat => cat.Id == catedratico.Id);
                if (existingCatedratico == null)
                {
                    throw new Exception("Catedratico not found");
                }
                _context.Set<Catedratico>().Remove(existingCatedratico);
                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al eliminar catedratico", e);
            }
        }
        public List<Catedratico> GetAllCatedraticos()
        {
            try
            {
                return _context.Set<Catedratico>().Include(catedratico => catedratico.ExperienciasEducativas)
                    .ThenInclude(experiencia => experiencia.ProgramaEducativo).ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener todos los catedraticos", e);
            }
        }
    }
}
