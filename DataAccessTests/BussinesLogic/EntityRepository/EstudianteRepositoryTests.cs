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
    public class EstudianteRepositoryTests
    {
        static TutoriasContext context = new TutoriasContext();
        private EstudianteRepository estudianteRepository = new EstudianteRepository(context);
        private static TutorAcademicoRepository tutorAcademicoRepository= new TutorAcademicoRepository(context);

        private Estudiante estudiante = new Estudiante()
        {
            Nombres = "Paulito Y Su Norteño banda",
            Apellidos = "Del norte para el mundo",
            Matricula = "123685",
            TutorAcademico = tutorAcademicoRepository.GetTutorAcademico(1)
        };

        [TestMethod()]
        public void AddEstudianteTest()
        {
            Assert.IsTrue(estudianteRepository.AddEstudiante(estudiante));
        }

        [TestMethod()]
        public void DeleteEstudianteTest()
        {
            estudiante = estudianteRepository.GetEstudianteById(2);
            Assert.IsTrue(estudianteRepository.DeleteEstudiante(estudiante));
        }

        [TestMethod()]
        public void UpdateEstudianteTest()
        {
            estudiante = estudianteRepository.GetEstudianteById(3);
            estudiante.Nombres = "Paulito serio";
            Assert.IsTrue(estudianteRepository.UpdateEstudiante(estudiante));
        }

        [TestMethod()]
        public void GetAllEstudiantesTest()
        {
            List<Estudiante> result = estudianteRepository.GetAllEstudiantes();
            result.ForEach(x => Console.WriteLine(x.Nombres + "\n") );
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetEstudiantesWithOutTutorTest()
        {
            List<Estudiante> result = estudianteRepository.GetEstudiantesWithOutTutor();
            result.ForEach(x => Console.WriteLine(x.Nombres + "\n"));
            Assert.IsNotNull(result);
        }
    }
}