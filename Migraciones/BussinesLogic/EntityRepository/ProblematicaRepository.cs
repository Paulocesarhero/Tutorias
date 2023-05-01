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
    public class ProblematicaRepository : IProblematicasRepository
    {
        private readonly DbContext _context;

        public ProblematicaRepository(DbContext context)
        {
            _context = context;
        }

        public bool AddProblematica(Problematica problematica)
        {
            try
            {
                Problematica problematicaToAdd = new Problematica()
                {
                    Descripcion = problematica.Descripcion,
                    NumAlumnos = problematica.NumAlumnos,
                    ExperienciaEducativa = _context.Set<Experiencia_Educativa>()
                        .FirstOrDefault(exp => exp.Nrc == problematica.ExperienciaEducativa.Nrc),
                    ReporteDeTutoria = _context.Set<Reporte_De_Tutoria>()
                        .FirstOrDefault(rep => rep.Id == problematica.ReporteDeTutoria.Id)
                };
                _context.Set<Problematica>().Add(problematicaToAdd);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al agregar problematica", e);
            }
          
        }

        public Problematica GetProblematica(int id)
        {
            try
            {
                return _context.Set<Problematica>().
                    Include(x => x.Solucion)
                    .FirstOrDefault(x => x.Id == id);
            }
            catch (DbException e)
            {
                throw new Exception("Error al eliminar Problematica", e);
            }
        }

        public bool UpdateProblematica(Problematica problematica)
        {
            try
            {
                Problematica existingProblematica = _context.Set<Problematica>().FirstOrDefault(prob => prob.Id == problematica.Id);
                if (existingProblematica == null)
                {
                    throw new Exception("No se encontro la problematica");
                }
                _context.Set<Problematica>().Update(problematica);
                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al actualizar problematica", e);
            }
        }

        public bool DeleteProblematica(Problematica problematica)
        {
            try
            {
               
                _context.Set<Problematica>().Remove(problematica);
                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al eliminar Problematica", e);
            }
        }

        public List<Problematica> FindProblematicasaWithoutSolucion(Periodo_Escolar periodoEscolarSeleccionado, int numDeSesion)
        {
            try
            {
                return _context.Set<Problematica>()
                    .Where(x => x.ReporteDeTutoria.FechaDeTutoria.PeriodoEscolar == periodoEscolarSeleccionado
                                && x.ReporteDeTutoria.FechaDeTutoria.NumDeTutoria == numDeSesion && x.Solucion == null)
                    .Include(c => c.ExperienciaEducativa)
                    .ThenInclude(e => e.Catedratico)
                    .Include(e => e.ExperienciaEducativa)
                    .ThenInclude(c => c.Academia)
                    .Include(c => c.ReporteDeTutoria)
                    .Include(c => c.Solucion)
                    .ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al encontrar problematicas sin solucion", e);

            }
        }

        public List<Problematica> GetProblematicas(Periodo_Escolar periodoEscolarSeleccionado, int numDeSesion)
        {
            try
            {
                return _context.Set<Problematica>()
                    .Where(x => x.ReporteDeTutoria.FechaDeTutoria.PeriodoEscolar == periodoEscolarSeleccionado
                                && x.ReporteDeTutoria.FechaDeTutoria.NumDeTutoria == numDeSesion)
                    .Include(c => c.ExperienciaEducativa)
                    .ThenInclude(e => e.Catedratico)
                    .Include(e => e.ExperienciaEducativa)
                    .ThenInclude(c => c.Academia)
                    .Include(c => c.ReporteDeTutoria)
                    .Include(c => c.Solucion)
                    .ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al buscar problematicas", e);

            }
            
        }

        public List<Problematica> GetProblematicaByExperienciaEducativa(string nrc)
        {
            try
            {
                return _context.Set<Problematica>()
                    .Where(x => x.ExperienciaEducativa.Nrc == nrc)
                    .Include(c => c.ExperienciaEducativa)
                    .ThenInclude(e => e.Catedratico)
                    .Include(e => e.ExperienciaEducativa)
                    .ThenInclude(c => c.Academia)
                    .Include(c => c.ReporteDeTutoria)
                    .Include(c => c.Solucion)
                    .ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al buscar problematicas", e);

            }
        }

        public List<Problematica> GetProblematicasByFechaDeTutoria(Fecha_De_Tutoria fechaDeTutoria, Tutor_Academico tutorAcademico)
        {
            try
            {
                return _context.Set<Problematica>()
                    .Where(x => x.ReporteDeTutoria.FechaDeTutoria == fechaDeTutoria && x.ReporteDeTutoria.TutorAcademico == tutorAcademico)
                    .Include(c => c.ExperienciaEducativa)
                    .ThenInclude(e => e.Catedratico)
                    .Include(e => e.ExperienciaEducativa)
                    .ThenInclude(c => c.Academia)
                    .Include(c => c.ReporteDeTutoria)
                    .Include(c => c.Solucion)
                    .ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al buscar problematicas", e);

            }
        }
    }
}
