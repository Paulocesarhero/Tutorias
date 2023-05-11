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
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly DbContext _context;

        public EstudianteRepository(DbContext context)
        {
            _context = context;
        }

        public bool AddEstudiante(Estudiante estudiante)
        {
            try
            {
                Estudiante exist = _context.Set<Estudiante>()
                    .FirstOrDefault(x => x.Id == estudiante.Id || x.Matricula == estudiante.Matricula);
                if (exist != null)
                {
                    return true;
                }
                _context.Set<Estudiante>().Add(estudiante);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                 throw new ("Error al agregar al estudiante", e);
            }
        }

        public bool DeleteEstudiante(Estudiante estudiante)
        {
            try
            {
                Estudiante exist = _context.Set<Estudiante>()
                    .FirstOrDefault(x => x.Id == estudiante.Id || x.Matricula == estudiante.Matricula);
                if (exist == null)
                {
                    throw new Exception("Estudiante no encontrado");
                }
                _context.Set<Estudiante>().Remove(exist);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new("Error al eliminar el estudiante", e);
            }
        }

        public bool UpdateEstudiante(Estudiante estudiante)
        {
            try
            {
                Estudiante exist = _context.Set<Estudiante>()
                    .FirstOrDefault(x => x.Id == estudiante.Id || x.Matricula == estudiante.Matricula);
                if (exist == null)
                {
                    throw new Exception("Estudiante no encontrado");
                }

                _context.Set<Estudiante>().Update(exist);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new("Error al actualizar el estudiante", e);

            }
        }

        public List<Estudiante> GetAllEstudiantes()
        {
            try
            {
                return _context.Set<Estudiante>().ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener los estudiantes", e);

            }
        }

        public List<Estudiante> GetEstudiantesWithOutTutor()
        {
            try
            {
                return _context.Set<Estudiante>().Where(e=> e.TutorAcademico == null)
                    .Include(x => x.TutorAcademico)
                    .ThenInclude(x => x.Usuario)
                    .ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener los estudiantes", e);

            }
        }

        public Estudiante GetEstudianteById(int id)
        {
            try
            {
                return _context.Set<Estudiante>().Where(e => e.Id == id).FirstOrDefault();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener los estudiantes", e);

            }
        }

        public List<Estudiante> GetEstudiantesByTutorAcademico(Tutor_Academico tutorAcademico)
        {
            try
            {
                return _context.Set<Estudiante>().Where(e => e.TutorAcademico.Id == tutorAcademico.Id).ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener los estudiantes", e);

            }
        }

        public List<Estudiante> findEstudiantesWithOutTutor()
        {
            try
            {
                return _context.Set<Estudiante>().Where(e => e.TutorAcademico == null)
                    .Include(x => x.ProgramaEducativo)
                    .ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener los estudiantes", e);
            }
        }

        public bool updateAssignmentTutorToStudent(Estudiante objetoEstudiante)
        {
            bool status = false;
            using (TutoriasContext context = new TutoriasContext())
            {
                try
                {
                    context.Estudiantes.Update(objetoEstudiante);
                    context.SaveChanges();
                    status = true;
                }
                catch (DbUpdateException dbUpdateException)
                {
                    throw dbUpdateException;
                }
                catch (InvalidOperationException invalidOperationException)
                {
                    throw invalidOperationException;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return status;
        }

        public List<Estudiante> getEstudiantesWithTutor(Tutor_Academico tutorRecibido)
        {
            try
            {
                return _context.Set<Estudiante>().Where(e => e.TutorAcademico.Id == tutorRecibido.Id)
                    .Include(x => x.TutorAcademico)
                    .ThenInclude(x => x.Usuario)
                    .ThenInclude(x => x.ProgramaEducativo)
                    .ToList();
            }
            catch (DbException e)
            {
                throw new Exception("Error al obtener los estudiantes", e);
            }
        }

        public bool UpdateTutorToEstudiante(Estudiante estudiante)
        {
            try
            {
                Estudiante exist = _context.Set<Estudiante>()
                    .FirstOrDefault(x => x.Id == estudiante.Id || x.Matricula == estudiante.Matricula);
                if (exist == null)
                {
                    throw new Exception("Estudiante no encontrado");
                }

                exist.IdTutorAcademico = estudiante.IdTutorAcademico;

                _context.Set<Estudiante>().Update(exist);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException e)
            {
                throw new("Error al actualizar el estudiante", e);

            }
        }

    }
}
