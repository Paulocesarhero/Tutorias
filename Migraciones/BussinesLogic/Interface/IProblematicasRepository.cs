using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface IProblematicasRepository
    {
        bool AddProblematica(Problematica problematica);
        bool UpdateProblematica(Problematica problematica);
        bool DeleteProblematica(Problematica problematica);
        List<Problematica> FindProblematicasaWithoutSolucion(Periodo_Escolar periodoEscolarSeleccionado,
            int numDeSesion);
        List<Problematica> GetProblematicas(Periodo_Escolar periodoEscolarSeleccionado, int numDeSesion);
        List<Problematica> GetProblematicaByExperienciaEducativa(string nrc);
        Problematica GetProblematica(int id);

        List<Problematica> GetProblematicasByFechaDeTutoria(Fecha_De_Tutoria fechaDeTutoria,
            Tutor_Academico tutorAcademico);

    }
} 
