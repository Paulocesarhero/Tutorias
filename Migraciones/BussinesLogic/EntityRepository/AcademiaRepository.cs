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
    public class AcademiaRepository: IAcademiaRepository
    {
        private readonly DbContext _context;

        public AcademiaRepository(DbContext context)
        {
            _context = context;
        }

        public bool AddAcademia(Academia academia)
        {
            try
            {
                Academia exist = _context.Set<Academia>().FirstOrDefault(x => x.NombreAcademia == academia.NombreAcademia);
                if (exist != null)
                {
                    return true;
                }
                _context.Set<Academia>().Add(academia);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al agregar la academia", e);
            }
        }

        public bool DeleteAcademia(Academia academia)
        {
            try
            {
                Academia exist = _context.Set<Academia>().FirstOrDefault(x => x.NombreAcademia == academia.NombreAcademia);
                if (exist == null)
                {
                    throw new Exception("No se encontro a la academia");
                }
                _context.Set<Academia>().Remove(academia);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al agregar la academia", e);
            }
        }

        public List<Academia> getAllAcademias()
        {
            try
            {
                return _context.Set<Academia>().ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener las academias", e);
            }
        }

        public bool UpdateAcademia(Academia academia)
        {
            try
            {
                Academia exist = _context.Set<Academia>().FirstOrDefault(x => x.Id == academia.Id);
                if (exist == null)
                {
                    throw new Exception("No se encontro a la academia");
                }
                _context.Set<Academia>().Update(academia);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al agregar la academia", e);
            }
        }
    }
}
