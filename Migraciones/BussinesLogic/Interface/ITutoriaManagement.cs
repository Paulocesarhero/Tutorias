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
		bool AddProblematica(Problematica problematica, Experiencia_Educativa experienciaEducatica, Reporte_De_Tutoria reporteDeTutoria);
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

		

		List<Catedratico> getCatedraticos();
		List<Experiencia_Educativa> findExperienciaEducativaWithoutCatedratico();

		List<Periodo_Escolar> getPeriodosEscolares();

		public List<Problematica> findProblematicasAcademicas(Periodo_Escolar periodoEscolarSeleccionado,
			int numDeSesion);






	}
}
