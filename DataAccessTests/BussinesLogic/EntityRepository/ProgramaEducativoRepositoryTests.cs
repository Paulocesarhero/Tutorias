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
    public class ProgramaEducativoRepositoryTests
    {
        private Programa_Educativo programaEducativo = new Programa_Educativo("REDES");
        private static TutoriasContext tutorias = new TutoriasContext();
        private ProgramaEducativoRepository programaEducativoRepository = new ProgramaEducativoRepository(tutorias);



        [TestMethod()]
        public void AddProgramaEducativoTest()
        {
            Assert.IsTrue(programaEducativoRepository.AddProgramaEducativo(programaEducativo));
        }
        [TestMethod()]
        public void GetProgramasEducativosTest()
        {
            Assert.IsNotNull(programaEducativoRepository.GetProgramasEducativos());
        }

        [TestMethod()]
        public void DeleteProgramaEducativoTest()
        {
            Assert.IsTrue(programaEducativoRepository.DeleteProgramaEducativo(programaEducativo));
        }
    }
}