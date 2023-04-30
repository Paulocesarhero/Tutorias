using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BussinesLogic.Interface;
using Tutorias.Service.DatabaseContext;
using System.Data.Common;

namespace DataAccess.BussinesLogic.EntityRepository
{
    public class SolucionRepository : ISolucionesRepository
    {
        private readonly DbContext _context;

        public SolucionRepository(DbContext context)
        {
            _context = context;
        }

        public bool AddSolucion(Solucion solucion)
        {
            bool result = false;
            Solucion findSol = new Solucion();
            try
            {

                findSol = _context.Set<Solucion>().FirstOrDefault(x => x.Id == solucion.Id);
                if (findSol == null)
                {
                    _context.Set<Solucion>().Update(solucion);
                }
                else
                {
                    _context.Entry(findSol).CurrentValues.SetValues(solucion);
                }

                if (_context.SaveChanges() > 0)
                {
                    result = true;
                }
            }
            catch (DbException dbException)
            {
                throw new Exception("Error al agregar la solucion", dbException);
            }
            return result;
        }

        public Solucion GetSolucion(int id)
        {
            try
            {
                return _context.Set<Solucion>().FirstOrDefault(x => x.Id == id);
            }
            catch (DbException dbException)
            {
                throw new Exception("Error al obtener la solucion", dbException);
            }
        }

    }
}
