using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
			bool status;
			using (TutoriasContext context = new TutoriasContext())
			{
				try
				{
					context.ExperienciasEducativas.Add(experienciaEducativa);
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

		public bool AddProblematica(Problematica problematica, Experiencia_Educativa experienciaEducatica,
			Reporte_De_Tutoria reporteDeTutoria)
		{
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
		}

		public Usuario Login(string usename, string password)
		{
			Usuario usuario = new Usuario();
			try
			{
				using (TutoriasContext context = new TutoriasContext())
				{
					usuario = context.Usuarios
						.Where(x => x.username == usename
						            && x.password == Security.HashSHA256(password))
						.Include(c => c.tipoUsuario)
						.Include(c => c.ProgramaEducativo)
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
						.Include(c => c.experienciasEducativas)
						.ThenInclude(x => x.programaEducativo)
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
						.Where(x => x.reporteDeTutoria.periodoEscolar == periodoEscolarSeleccionado
						            && x.reporteDeTutoria.numDeTutoria == numDeSesion)
						.Include(c => c.experienciaEducativa)
						.Include(c => c.reporteDeTutoria)
						.Include(c => c.solucion)
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

		public Experiencia_Educativa getExperienciaEducativaByNRC(string nrc)
		{
			Experiencia_Educativa result = new Experiencia_Educativa();
			try
			{
				using (TutoriasContext context = new TutoriasContext())
				{
					result = context.ExperienciasEducativas
						.Where(x => x.nrc == nrc)
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

		public List<Reporte_De_Tutoria> findReportesDeTutorias(Periodo_Escolar periodoEscolarSeleccionado, int numDeSesion)
		{
			List<Reporte_De_Tutoria> reporteDeTutorias = new List<Reporte_De_Tutoria>();
			try
			{
				using (TutoriasContext context = new TutoriasContext())
				{
					reporteDeTutorias = context.ReportesDeTutorias
						.Where(x => x.periodoEscolar == periodoEscolarSeleccionado
						            && x.numDeTutoria == numDeSesion)
						.Include(c => c.tutorAcademico)
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
			return reporteDeTutorias;
		}

		public List<Experiencia_Educativa> getExperienciasEducativas()
		{
			List<Experiencia_Educativa> result = new List<Experiencia_Educativa>();
			try
			{
				using (TutoriasContext context = new TutoriasContext())
				{
					result = context.ExperienciasEducativas
						.Include(c => c.catedratico)
						.Include(c => c.programaEducativo)
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
	}
}
