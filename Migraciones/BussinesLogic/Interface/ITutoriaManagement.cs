using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Tutorias.Service.DatabaseContext;

namespace Tutorias.BussinesLogic.Interface
{
	public interface ITutoriaManagement
	{
		bool AddCatedratico(Catedratico catedratico);
		bool AddTutorAcademico(Tutor_Academico tutorAcademico);
		bool AddEstudiante(Estudiante estudiante);
		bool AddExperienciaEducativa(Experiencia_Educativa experienciaEducativa);
		bool AddReporteDeTutoria(Reporte_De_Tutoria reporteDeTutoria);

		bool AddPeriodoEscolar(Periodo_Escolar periodoEscolar);
		bool ModCatedratico(Catedratico catedratico);
		bool ModExperienciaEducativa(Experiencia_Educativa experienciaEducativa);

		bool AllocateExperienciaEducativaToCatedratico(Experiencia_Educativa experienciaEduactiva,
			Catedratico catedratico);
		bool AllocateEstudianteToTutorAcademico(Estudiante estudiante, Tutor_Academico tutorAcademico);
		bool AllocateSolucionToProblematica(Solucion solucion, Problematica problemtica);

		bool DeleteCatedratico(Catedratico catedratico);
		bool DeleteExperienciaEduactiva(Experiencia_Educativa experienciaEducativa);


		Usuario Login(string usename, string password);
		bool AddUSer(Usuario usuario);

		

		List<Catedratico> GetCatedraticos();
		List<Experiencia_Educativa> FindExperienciaEducativaWithoutCatedratico();

		List<Periodo_Escolar> GetPeriodosEscolares();

		public List<Problematica> FindProblematicasAcademicas(Periodo_Escolar periodoEscolarSeleccionado,
			int numDeSesion);






	}
}
