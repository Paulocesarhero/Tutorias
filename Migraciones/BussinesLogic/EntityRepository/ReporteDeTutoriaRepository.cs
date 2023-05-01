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
    public class ReporteDeTutoriaRepository : IReportesDeTutoriasRepository
    {
        private readonly DbContext _context;

        public ReporteDeTutoriaRepository(DbContext context)
        {
            _context = context;
        }

        public bool AddReporteDeTutoria(Reporte_De_Tutoria reporteDeTutoria)
        {
            try
            {
                Reporte_De_Tutoria exist = _context.Set<Reporte_De_Tutoria>().FirstOrDefault(x => x.FechaDeTutoria == reporteDeTutoria.FechaDeTutoria);
                if (exist == null)
                {
                    FechaDeTutoriaRepository fechaDeTutoriaRepository = new FechaDeTutoriaRepository(_context);

                    Reporte_De_Tutoria reporteToAdd = new Reporte_De_Tutoria()
                    {
                        Fecha = reporteDeTutoria.Fecha,
                        Comentarios = reporteDeTutoria.Comentarios,
                        FechaDeTutoria = fechaDeTutoriaRepository.GetFechaDeTutoriaActual(reporteDeTutoria.Fecha),
                        TutorAcademico = _context.Set<Tutor_Academico>().FirstOrDefault(x => x.Id == reporteDeTutoria.TutorAcademico.Id)
                    };
                    _context.Set<Reporte_De_Tutoria>().Add(reporteToAdd);
                }
                else
                {
                    exist.FechaDeTutoria = reporteDeTutoria.FechaDeTutoria;
                    exist.TutorAcademico = reporteDeTutoria.TutorAcademico;
                    exist.Comentarios = reporteDeTutoria.Comentarios;
                    exist.Fecha = reporteDeTutoria.Fecha;
                    _context.Set<Reporte_De_Tutoria>().Update(exist);
                }
                
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al agregar reporte de tutoria", e);
            }

        }

        public Reporte_De_Tutoria GetReporteDeTutoria(Tutor_Academico tutorAcademico, Fecha_De_Tutoria fechaDeTutoria)
        {
            try
            {
                return _context.Set<Reporte_De_Tutoria>().FirstOrDefault(x =>
                    x.TutorAcademico.Id == tutorAcademico.Id && x.FechaDeTutoria.Id == fechaDeTutoria.Id);
            }
            catch (DbException e)
            {
                throw new Exception("Error al agregar reporte de tutoria", e);
            }
        }

        public List<Reporte_De_Tutoria> GetReporteDeTutoriaByPeriodo(Periodo_Escolar periodoEscolar, int numDeTutoria)
        {
            try
            {
                return _context.Set<Reporte_De_Tutoria>()
                    .Where(x =>x.FechaDeTutoria.PeriodoEscolar == periodoEscolar && x.FechaDeTutoria.NumDeTutoria == numDeTutoria)
                    .Include(x => x.TutorAcademico)
                    .Include(x => x.FechaDeTutoria)
                    .ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al agregar reporte de tutoria", e);
            }
        }
    }
}
