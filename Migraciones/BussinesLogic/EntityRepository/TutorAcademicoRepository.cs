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
    public class TutorAcademicoRepository : ITutorAcademicoRepository
    {
        private readonly DbContext _context;

        public TutorAcademicoRepository(DbContext context)
        {
            _context = context;
        }

        public bool AddTutorAcademico(Tutor_Academico tutorAcademico)
        {
            try
            {
                Tutor_Academico exist =  _context.Set<Tutor_Academico>()
                    .FirstOrDefault(x => (x.Nombres == tutorAcademico.Nombres) && (x.Apellidos == tutorAcademico.Apellidos) || x.Id == tutorAcademico.Id);
                if (exist != null)
                {
                    return true;
                }

                _context.Add(tutorAcademico);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al agregar tutor academico", e);
            }
        }

        public bool DeleteTutorAcademico(Tutor_Academico tutorAcademico)
        {
            try
            {
                Tutor_Academico exist = _context.Set<Tutor_Academico>()
                    .FirstOrDefault(x => x.Id == tutorAcademico.Id);
                if (exist == null)
                {
                     throw new Exception("Tutor academico no encontrado");
                }

                _context.Remove(exist);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al agregar tutor academico", e);
            }
        }

        public bool UpdateTutorAcademico(Tutor_Academico tutorAcademico)
        {
            try
            {
                Tutor_Academico exist = _context.Set<Tutor_Academico>()
                    .FirstOrDefault(x => x.Id == tutorAcademico.Id);
                if (exist == null)
                {
                    throw new Exception("Tutor academico no encontrado");
                }

                _context.Update(tutorAcademico);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error al agregar tutor academico", e);
            }
        }

        public Tutor_Academico GetTutorAcademico(int id)
        {
            try
            {
                return _context.Set<Tutor_Academico>()
                    .Include(x => x.Estudiantes)
                    .FirstOrDefault(x => x.Id == id);
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener el tutor academico de la base de datos", e);
            }
        }

        public List<Tutor_Academico> GetAllTutorAcademico()
        {
            try
            {
                return _context.Set<Tutor_Academico>()
                    .Include(x => x.Estudiantes)
                    .Include(x => x.Usuario).ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener el tutor academico de la base de datos", e);
            }
        }

        public Tutor_Academico GetTutorAcademicoByUser(Usuario usuario)
        {
            try
            {
                return _context.Set<Tutor_Academico>()
                    .Include(x => x.Estudiantes)
                    .FirstOrDefault(t => t.Usuario.Id == usuario.Id);
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener el tutor academico de la base de datos", e);
            }
        }
    }
}
