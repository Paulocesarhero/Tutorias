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
    public class AsistenciaRepository : IAsistenciasRepository
    {
        private readonly DbContext _context;

        public AsistenciaRepository(DbContext context)
        {
            _context = context;
        }

        public int GetTotalDeAsistencias(Periodo_Escolar periodoEscolar, int numDeSesion)
        {
            try
            {
                return _context.Set<Asistencia>()
                    .Where(x => x.FechaDeTutoria.PeriodoEscolar == periodoEscolar
                                && x.FechaDeTutoria.NumDeTutoria == numDeSesion)
                    .ToList().Count;
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener total de asistencias", e);
            }
        }

        public bool AddAsistencia(Asistencia asistencia)
        {
            try
            {
                _context.Set<Asistencia>().Attach(asistencia);
                _context.Entry(asistencia).State = EntityState.Modified;

                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al agregar asistencias", e);
            }
        }

        public bool DeleteAsistencia(Asistencia asistencia)
        {
            try
            {
                _context.Set<Asistencia>().Remove(asistencia);
                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al eliminar asistencias", e);
            }
        }


        public List<Asistencia> GetAllAsistencias()
        {
            try
            {
                return _context.Set<Asistencia>()
                    .Include(x=> x.Estudiante)
                    .Include(x => x.FechaDeTutoria)
                    .ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener las asistencias", e);
            }
        }

        public List<Asistencia> GetAsistencias(Tutor_Academico tutorAcademico, Fecha_De_Tutoria fechaDeTutoria)
        {
            try
            {
                return _context.Set<Asistencia>()
                    .Include(x => x.Estudiante)
                    .Include(x => x.FechaDeTutoria)
                    .ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener las asistencias", e);
            }
        }

        public bool AddAsistencia(Fecha_De_Tutoria fechaDeTutoria, List<Estudiante> estudiantes)
        {
            throw new NotImplementedException();
        }
    }
}
