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
    public class SolucionRepositoryTests
    {
        private static TutoriasContext tutoriasContext = new TutoriasContext();
        private ProblematicaRepository problematicaRepository = new ProblematicaRepository(tutoriasContext);
        private SolucionRepository solucionRepository = new SolucionRepository(tutoriasContext);

        private Solucion solucion = new Solucion()
        {
            Titulo = "Se tomara una decisión en conjunto",
            Descripcion = "Se pasara un reporte a la academia",
            Fecha = DateTime.Now
        };

        [TestMethod()]
        public void AddSolucionTest()
        {
            solucion.Problematica = problematicaRepository.GetProblematica(3);
            Assert.IsTrue(solucionRepository.AddSolucion(solucion));
        }


    }
}