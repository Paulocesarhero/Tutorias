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
    public class FechaDeTutoriaRepositoryTests
    {
        private static TutoriasContext tutoriasContext = new TutoriasContext();
        private FechaDeTutoriaRepository fechaDeTutoriaRepository = new FechaDeTutoriaRepository(tutoriasContext);
        [TestMethod()]
        public void GetFechaDeTutoriaActualTest()
        {
            Console.WriteLine(fechaDeTutoriaRepository.GetFechaDeTutoriaActual(DateTime.Now).FechaDeCierre);
        }
    }
}