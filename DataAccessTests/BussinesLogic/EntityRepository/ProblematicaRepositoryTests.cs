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
    public class ProblematicaRepositoryTests
    {
        private static TutoriasContext tutoriasContext = new TutoriasContext();
        private ProblematicaRepository problematicaRepository  = new ProblematicaRepository(tutoriasContext);

        private Problematica problematica = new Problematica()
        {
            Descripcion = "Los alumnos no se sienten comodos con compartir ideas con los maestros",
            NumAlumnos = 2,
            ExperienciaEducativa = new Experiencia_Educativa()
            {
                Nrc = "78280"
            },
            ReporteDeTutoria = new Reporte_De_Tutoria
            {
                Id = 1
            }

        };
        [TestMethod()]
        public void AddProblematicaTest()
        {
            Assert.IsTrue(problematicaRepository.AddProblematica(problematica));
        }

        [TestMethod()]
        public void UpdateProblematicaTest()
        {
            problematica = problematicaRepository.GetProblematica(2);
            problematica.Descripcion = "Los alumno se duermen en las clases";
            Assert.IsTrue(problematicaRepository.UpdateProblematica(problematica));
        }

        [TestMethod()]
        public void DeleteProblematicaTest()
        {
            problematica = problematicaRepository.GetProblematica(2);
            Assert.IsTrue(problematicaRepository.DeleteProblematica(problematica));
        }

        [TestMethod()]
        public void FindProblematicasaWithoutSolucionTest()
        {
            Periodo_Escolar prEscolar = new Periodo_Escolar()
            {
                Id = 1
            };
            problematicaRepository.FindProblematicasaWithoutSolucion(prEscolar, 1).ForEach(x => Console.WriteLine(x.Id + " " +x.Descripcion));

            Assert.IsNotNull(problematicaRepository.FindProblematicasaWithoutSolucion(prEscolar, 1));
        }

        [TestMethod()]
        public void GetProblematicasTest()
        {
            Periodo_Escolar prEscolar = new Periodo_Escolar()
            {
                Id = 1
            };
            problematicaRepository.GetProblematicas(prEscolar, 1).ForEach(x => Console.WriteLine(x.Id + " " + x.Descripcion));

            Assert.IsNotNull(problematicaRepository.GetProblematicas(prEscolar, 1));
        }

        [TestMethod()]
        public void GetProblematicaByExperienciaEducativaTest()
        {
            Periodo_Escolar prEscolar = new Periodo_Escolar()
            {
                Id = 1
            };
            problematicaRepository.GetProblematicaByExperienciaEducativa("78280").ForEach(x => Console.WriteLine(x.Id + " " + x.Descripcion));

            Assert.IsNotNull(problematicaRepository.GetProblematicaByExperienciaEducativa("78280"));
        }
    }
}