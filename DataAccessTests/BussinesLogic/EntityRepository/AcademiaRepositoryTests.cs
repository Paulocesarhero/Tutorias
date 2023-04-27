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
    public class AcademiaRepositoryTests
    {
        static TutoriasContext tutoriasContext = new TutoriasContext();
        private AcademiaRepository academiaRepository = new AcademiaRepository(tutoriasContext);

        private Academia academia = new Academia()
        {
            NombreAcademia = "Q&A"
        };

        [TestMethod()]
        [Priority(1)]
        public void AddAcademiaTest()
        {
            Assert.IsTrue(academiaRepository.AddAcademia(academia));
        }
        [TestMethod()]
        [Priority(2)]
        public void UpdateAcademiaTest()
        {
            List<Academia> result = academiaRepository.getAllAcademias();
            result.Last().NombreAcademia = "Verificacion y validacion";
            Assert.IsTrue(academiaRepository.UpdateAcademia(result.Last()));
        }
        [TestMethod()]
        public void getAllAcademiasTest()
        {
            List<Academia> result = academiaRepository.getAllAcademias();
            result.ForEach(x => Console.WriteLine(x.NombreAcademia + "\n"));
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DeleteAcademiaTest()
        {
            List<Academia> result = academiaRepository.getAllAcademias();
            Assert.IsTrue(academiaRepository.DeleteAcademia(result.Last()));
        }

        
    }
}