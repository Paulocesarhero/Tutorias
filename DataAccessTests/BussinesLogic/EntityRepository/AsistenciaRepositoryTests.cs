using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.BussinesLogic.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.EntityRepository.Tests
{
    [TestClass()]
    public class AsistenciaRepositoryTests
    {
        static TutoriasContext tutoriasContext = new TutoriasContext();
        private AsistenciaRepository asistenciaRepository = new AsistenciaRepository(tutoriasContext);
        private static EstudianteRepository estudianteRepository = new EstudianteRepository(tutoriasContext);
        private static FechaDeTutoriaRepository fechaDeTutoriaRepository = new FechaDeTutoriaRepository(tutoriasContext);

        private static PeriodoEscolarRepository periododEscolarRepository =
            new PeriodoEscolarRepository(tutoriasContext);

        private Asistencia asistencia = new Asistencia()
        {
            Asiste = true,
            Horario = DateTime.Now,
            Estudiante = estudianteRepository.GetAllEstudiantes().Last(),
            FechaDeTutoria = fechaDeTutoriaRepository.GetFechaDeTutoriaActual(DateTime.Now),
        };
        [TestMethod()]
        public void GetTotalDeAsistenciasTest()
        {
            Periodo_Escolar periodoEscolarToFind = periododEscolarRepository.GetAllPeriodosEscolar().Last();
            Assert.IsNotNull(asistenciaRepository.GetTotalDeAsistencias(periodoEscolarToFind,1));
        }

        [TestMethod()]
        [Priority(1)]
        public void AddAsistenciaTest()
        {
            Assert.IsTrue(asistenciaRepository.AddAsistencia(asistencia));
        }

        [TestMethod()]
        public void DeleteAsistenciaTest()
        {
            Asistencia asistenciaToDelete = asistenciaRepository.GetAllAsistencias().Last();
            Assert.IsTrue(asistenciaRepository.DeleteAsistencia(asistenciaToDelete));
        }

        [TestMethod()]
        public void GetAllAsistenciasTest()
        {
            List<Asistencia> result = asistenciaRepository.GetAllAsistencias();
            result.ForEach(x=> Console.WriteLine(x.FechaDeTutoria.FechaDeCierre + " " + x.Estudiante.Matricula+ " " + x.Horario+ " " + x.Asiste+"\n"));
            Assert.IsNotNull(result);
        }
    }
}