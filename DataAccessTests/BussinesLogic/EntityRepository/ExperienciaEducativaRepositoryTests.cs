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
    public class ExperienciaEducativaRepositoryTests
    {
        private static TutoriasContext tutoriasContext = new TutoriasContext();
        private ExperienciaEducativaRepository experienciaEducativaRepository = new ExperienciaEducativaRepository(tutoriasContext);
        private Experiencia_Educativa experienciaEducativa = new Experiencia_Educativa()
        {
            Nombre = "DESARROLLO DE SISTEMAS EN RED",
            Nrc = "78280",
            Catedratico = new Catedratico
            {
                NombreCompleto = "JORGE OCTAVIO OCHARAN HERNADEZ"
            },
            Academia = new Academia("Desarrollos de sistema en red y aplicaciones"),
            ProgramaEducativo = new Programa_Educativo()
            {
                ProgramaEducativo = "Ingenieria de software"
            }
        };
        [TestMethod()]
        [Priority(1)]
        public void AddExperienciaEducativaTest()
        {
            Assert.IsTrue(experienciaEducativaRepository.AddExperienciaEducativa(experienciaEducativa));
        }


        [TestMethod()]
        [Priority(2)]

        public void GetByNrcTest()
        {
            string nrc = "78280";
            Experiencia_Educativa experienciaEducativa = experienciaEducativaRepository.GetByNrc("78280");
            Assert.AreEqual("78280", experienciaEducativa.Nrc);
        }

       

        [TestMethod()]
        [Priority(3)]
        public void UpdateExperienciaEducativaTest()
        {
            experienciaEducativa = experienciaEducativaRepository.GetByNrc("78280");
            experienciaEducativa.Nombre = "DESARROLLO DE SOFTWARE 122";
            Assert.IsTrue(experienciaEducativaRepository.UpdateExperienciaEducativa(experienciaEducativa));
        }

        [TestMethod()]
        [Priority(4)]
        public void DeleteExperienciaEducativaTest()
        {
            Experiencia_Educativa experiencia = experienciaEducativaRepository.GetByNrc("78280");
            Assert.IsTrue(experienciaEducativaRepository.DeleteExperienciaEducativa(experiencia));
        }
    }
}