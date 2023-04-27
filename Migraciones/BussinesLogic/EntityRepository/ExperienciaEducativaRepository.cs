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
    public class ExperienciaEducativaRepository : IExperienciasEducativasRepository
    {
        private readonly DbContext _context;

        public ExperienciaEducativaRepository(DbContext context)
        {
            _context = context;
        }

        public Experiencia_Educativa GetByNrc(string nrc)
        {
            try
            {
                return _context.Set<Experiencia_Educativa>()
                    .Include(x => x.ProgramaEducativo)
                    .Include(x => x.Academia)
                    .Include(x => x.Catedratico)
                    .FirstOrDefault(experiencia => experiencia.Nrc == nrc);
            }
            catch (DbException e)
            {
                throw new Exception("Error al buscar experiencia educativa", e);
            }
        }

        public bool AddExperienciaEducativa(Experiencia_Educativa experienciaEducativa)
        {
            try
            {
                Experiencia_Educativa experienciaEducativaToAdd = new Experiencia_Educativa
                {
                    Nombre = experienciaEducativa.Nombre,
                    Nrc = experienciaEducativa.Nrc,
                    ProgramaEducativo = _context.Set<Programa_Educativo>()
                        .FirstOrDefault(x => x.ProgramaEducativo == experienciaEducativa.ProgramaEducativo.ProgramaEducativo),
                    Academia = _context.Set<Academia>()
                        .FirstOrDefault(x => x.NombreAcademia == experienciaEducativa.Academia.NombreAcademia),
                    Catedratico = _context.Set<Catedratico>()
                        .FirstOrDefault(x => x.NombreCompleto == experienciaEducativa.Catedratico.NombreCompleto)
                };

                if (_context.Set<Experiencia_Educativa>().Any(x => x.Nrc == experienciaEducativa.Nrc))
                {
                    _context.Set<Experiencia_Educativa>().Update(experienciaEducativaToAdd);
                }
                else
                {
                    _context.Set<Experiencia_Educativa>().Add(experienciaEducativaToAdd);
                }

               return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al agregar catedratico", e);
            }
        }

        public bool UpdateExperienciaEducativa(Experiencia_Educativa experienciaEducativa)
        {
            try
            {
                
                _context.Set<Experiencia_Educativa>().Update(experienciaEducativa);
                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al actualizar catedratico", e);
            }
        }

        public bool DeleteExperienciaEducativa(Experiencia_Educativa experienciaEducativa)
        {
            try
            {
                Experiencia_Educativa exisitingExperienciaEducativa = _context.Set<Experiencia_Educativa>().FirstOrDefault(exp => exp.Nrc == experienciaEducativa.Nrc);
                if (exisitingExperienciaEducativa == null)
                {
                    throw new Exception("No se encontro la experiencia educativa");
                }
                _context.Set<Experiencia_Educativa>().Remove(exisitingExperienciaEducativa);
                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al eliminar catedratico", e);
            }
        }
        public List<Experiencia_Educativa> GetExperienciasEducativas()
        {
            try
            {
                return  _context.Set<Experiencia_Educativa>()
                        .Include(c => c.Catedratico)
                        .Include(c => c.ProgramaEducativo)
                        .Include(e => e.Academia)
                        .ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al buscar experiencia educativa", e);
            }
        }
    }
   
}
