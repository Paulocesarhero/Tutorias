using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;
using Sistema_De_Tutorias.Utility;
using Tutorias.BussinesLogic.Interface;
using Tutorias.Service.DatabaseContext;

namespace Tutorias.BussinesLogic.Management
{
	public class TutoriaManagement : ITutoriaManagement
	{


		public bool AddCatedratico(Catedratico catedratico)
		{
			bool status;
			using (TutoriasContext context = new TutoriasContext())
			{
                
				try
				{
					context.Catedraticos.Add(catedratico);
					context.SaveChanges();
					status = true;
				}
				catch (DbUpdateException dbUpdateException)
				{
					throw dbUpdateException;
				}
				catch(InvalidOperationException invalidOperationException)
				{
					throw  invalidOperationException;
				}
			}
			return status;
		}

		public bool AddTutorAcademico(Tutor_Academico tutorAcademico)
		{
			bool status;
			using (TutoriasContext context = new TutoriasContext())
			{
				try
				{
					context.TutorAcademico.Add(tutorAcademico);
					context.SaveChanges();
					status = true;
				}
				catch (DbUpdateException dbUpdateException)
				{
					throw dbUpdateException;
				}
				catch(InvalidOperationException invalidOperationException)
				{
					throw  invalidOperationException;
				}
			}
			return status;
		}

		public bool AddEstudiante(Estudiante estudiante)
		{
			bool status;
			using (TutoriasContext context = new TutoriasContext())
			{
				try
				{
					context.Estudiantes.Add(estudiante);
					context.SaveChanges();
					status = true;
				}
				catch (DbUpdateException dbUpdateException)
				{
					throw dbUpdateException;
				}
				catch(InvalidOperationException invalidOperationException)
				{
					throw  invalidOperationException;
				}
			}
			return status;
		}

        public bool AddExperienciaEducativa(Experiencia_Educativa experienciaEducativa)
        {
            using (TutoriasContext context = new TutoriasContext())
            {
                try
                {
                    Experiencia_Educativa experienciaEducativaToAdd = new Experiencia_Educativa
                    {
                        Nombre = experienciaEducativa.Nombre,
                        Nrc = experienciaEducativa.Nrc,
                        ProgramaEducativo = context.ProgramasEducativos
                            .FirstOrDefault(x => x.ProgramaEducativo == experienciaEducativa.ProgramaEducativo.ProgramaEducativo),
                        Academia = context.Academias
                            .FirstOrDefault(x => x.NombreAcademia == experienciaEducativa.Academia.NombreAcademia),
                        Catedratico = context.Catedraticos
                            .FirstOrDefault(x => x.NombreCompleto == experienciaEducativa.Catedratico.NombreCompleto)
                    };

                    if (context.ExperienciasEducativas.Any(x => x.Nrc == experienciaEducativa.Nrc))
                    {
                        context.ExperienciasEducativas.Update(experienciaEducativaToAdd);
                    }
                    else
                    {
                        context.ExperienciasEducativas.Add(experienciaEducativaToAdd);
                    }

                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception("Error al actualizar la experiencia educativa", ex);
                }
                catch (InvalidOperationException ex)
                {
                    throw new Exception("Error al actualizar la experiencia educativa", ex);
                }
            }
        }

     


        public bool AddReporteDeTutoria(Reporte_De_Tutoria reporteDeTutoria)
		{
			bool status;
			using (TutoriasContext context = new TutoriasContext())
			{
				try
				{
					context.ReportesDeTutorias.Update(reporteDeTutoria);
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

		public bool AddPeriodoEscolar(Periodo_Escolar periodoEscolar)
		{
			bool status;
			using (TutoriasContext context = new TutoriasContext())
			{
                
				try
				{
					context.PeriodosEscolares.Add(periodoEscolar);
					context.SaveChanges();
					status = true;
				}
				catch (DbUpdateException dbUpdateException)
				{
					throw dbUpdateException;
				}
				catch(InvalidOperationException invalidOperationException)
				{
					throw  invalidOperationException;
				}
			}
			return status;
		}

		public bool ModCatedratico(Catedratico catedratico)
		{
			throw new System.NotImplementedException();
		}

		public bool ModExperienciaEducativa(Experiencia_Educativa experienciaEducativa)
		{
			throw new System.NotImplementedException();
		}

		public bool AllocateExperienciaEducativaToCatedratico(Experiencia_Educativa experienciaEduactiva, Catedratico catedratico)
		{
			throw new System.NotImplementedException();
		}

		public bool AllocateEstudianteToTutorAcademico(Estudiante estudiante, Tutor_Academico tutorAcademico)
		{
			throw new System.NotImplementedException();
		}

		public bool AllocateSolucionToProblematica(Solucion solucion, Problematica problemtica)
		{
			throw new System.NotImplementedException();
		}

		public bool DeleteCatedratico(Catedratico catedratico)
		{
			throw new System.NotImplementedException();
		}

		public bool DeleteExperienciaEduactiva(Experiencia_Educativa experienciaEducativa)
        {
            try
            {
                using (TutoriasContext context = new TutoriasContext())
                {
                    context.ExperienciasEducativas.Remove(experienciaEducativa);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la experiencia educativa", ex);
            }
        }

		public Usuario Login(string usename, string password)
		{
			Usuario usuario = new Usuario();
			try
			{
				using (TutoriasContext context = new TutoriasContext())
				{
					usuario = context.Usuarios
						.Where(x => x.Username == usename
						            && x.Password == Security.HashSHA256(password))
						.Include(c => c.TipoUsuario)
						.Include(c => c.ProgramaEducativo)
						.FirstOrDefault();
				}
			}
			catch (DbException dbException)
			{
				throw new Exception("Error en el login", dbException);
			}
			catch (InvalidOperationException invalidOperationException)
			{
                throw new Exception("Error en el login", invalidOperationException);
			}
            return usuario;
		}

		public bool AddUSer(Usuario usuario)
		{
			throw new System.NotImplementedException();
		}

		public List<Catedratico> GetCatedraticos()
		{
			List<Catedratico> catedraticos = new List<Catedratico>();
			try
			{
				using (TutoriasContext context = new TutoriasContext())
				{
					catedraticos = context.Catedraticos
						.Include(c => c.ExperienciasEducativas)
						.ThenInclude(x => x.ProgramaEducativo)
						.ToList();
				}
			}
			catch (DbException dbException)
			{
				throw dbException;
			}
			catch (InvalidOperationException invalidOperationException)
			{
				throw invalidOperationException;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return catedraticos;
		}

		public List<Experiencia_Educativa> FindExperienciaEducativaWithoutCatedratico()
		{
			throw new System.NotImplementedException();
		}

		public List<Periodo_Escolar> GetPeriodosEscolares()
		{
			List<Periodo_Escolar> periodosEscolares = new List<Periodo_Escolar>();
			try
			{
				using (TutoriasContext context = new TutoriasContext())
				{
					periodosEscolares = context.PeriodosEscolares.ToList();
				}
			}
			catch (DbException dbException)
			{
				throw dbException;
			}
			catch (InvalidOperationException invalidOperationException)
			{
				throw invalidOperationException;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return periodosEscolares;
		}

		public bool AddProblematica(Problematica problematica)
		{
			bool status;
			using (TutoriasContext context = new TutoriasContext())
			{
                
				try
				{
					context.Problematicas.Update(problematica);
					context.SaveChanges();
					status = true;
				}
				catch (DbUpdateException dbUpdateException)
				{
					throw dbUpdateException;
				}
				catch(InvalidOperationException invalidOperationException)
				{
					throw  invalidOperationException;
				}
			}
			return status;
		}

		public void getProblematicasAcademicas()
		{
			throw new NotImplementedException();
		}

		public List<Problematica> FindProblematicasAcademicas(Periodo_Escolar periodoEscolarSeleccionado, int numDeSesion)
		{
			List<Problematica> problematicasResult = new List<Problematica>();
			try
			{
				using (TutoriasContext context = new TutoriasContext())
				{
					problematicasResult = context.Problematicas
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
			}
			catch (DbException dbException)
			{
				throw dbException;
			}
			catch (InvalidOperationException invalidOperationException)
			{
				throw invalidOperationException;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return problematicasResult;
		}

        public int FindTotalAssistants(Periodo_Escolar periodoEscolarSeleccionado, int numDeSesion)
        {
            List<Asistencia> findAsistencias = new List<Asistencia>();
            try
            {
                using (TutoriasContext context = new TutoriasContext())
                {
                    findAsistencias = context.Asistencias
                        .Where(x => x.FechaDeTutoria.PeriodoEscolar == periodoEscolarSeleccionado
                                    && x.FechaDeTutoria.NumDeTutoria == numDeSesion)
                        .ToList();
                }
            }
            catch (DbException dbException)
            {
                throw dbException;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                throw invalidOperationException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return findAsistencias.Count;
        }


        public Experiencia_Educativa getExperienciaEducativaByNRC(string nrc)
		{
			Experiencia_Educativa result = new Experiencia_Educativa();
			try
			{
				using (TutoriasContext context = new TutoriasContext())
				{
					result = context.ExperienciasEducativas
						.Where(x => x.Nrc == nrc)
						.FirstOrDefault();
				}
			}
			catch (DbException dbException)
			{
				throw dbException;
			}
			catch (InvalidOperationException invalidOperationException)
			{
				throw invalidOperationException;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		

		public List<Experiencia_Educativa> getExperienciasEducativas()
		{
			List<Experiencia_Educativa> result = new List<Experiencia_Educativa>();
			try
			{
				using (TutoriasContext context = new TutoriasContext())
				{
					result = context.ExperienciasEducativas
						.Include(c => c.Catedratico)
						.Include(c => c.ProgramaEducativo)
                        .Include(e=> e.Academia)
						.ToList();
				}
			}
			catch (DbException dbException)
			{
				throw dbException;
			}
			catch (InvalidOperationException invalidOperationException)
			{
				throw invalidOperationException;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public bool AddCatedraticos(List<Catedratico> catedraticos)
		{
			bool result;
			try
			{
				using (TutoriasContext context = new TutoriasContext())
				{
					context.Catedraticos.AttachRange(catedraticos);
					context.SaveChanges();
					result = true;
				}
			}
			catch (DbException dbException)
			{
				throw dbException;
			}
			catch (InvalidOperationException invalidOperationException)
			{
				throw invalidOperationException;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

        public bool AddSolucion(Solucion solucionProblematica)
        {
            bool result = false;
            Solucion findSol = new Solucion();
            try
            {
                using (TutoriasContext context = new TutoriasContext())
                {
                    findSol = context.Soluciones.FirstOrDefault(x => x.Id == solucionProblematica.Id);
                    if (findSol == null)
                    {
                        context.Soluciones.Update(solucionProblematica);
                    }
                    else
                    {
                        context.Entry(findSol).CurrentValues.SetValues(solucionProblematica);
                    }

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (DbException dbException)
            {
                throw dbException;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                throw invalidOperationException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<Academia> GetAcademias()
        {
            List<Academia> result = new List<Academia>();
            try
            {
                using (TutoriasContext context = new TutoriasContext())
                {
                    result = context.Academias
                        .ToList();
                }
            }
            catch (DbException dbException)
            {
                throw dbException;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                throw invalidOperationException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<Programa_Educativo> GetProgramasEducativos()
        {
            List<Programa_Educativo> result = new List<Programa_Educativo>();
            try
            {
                using (TutoriasContext context = new TutoriasContext())
                {
                    result = context.ProgramasEducativos
                        .ToList();
                }
            }
            catch (DbException dbException)
            {
                throw dbException;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                throw invalidOperationException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<Estudiante> findEstudiantesWithOutTutor()
        {
            List<Estudiante> result = new List<Estudiante>();
            try
            {
                using (TutoriasContext context = new TutoriasContext())
                {
                    result = context.Estudiantes.Where(estudiante => estudiante.TutorAcademico == null)
                        .ToList();
                }
            }
            catch (DbException dbException)
            {
                throw dbException;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                throw invalidOperationException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
