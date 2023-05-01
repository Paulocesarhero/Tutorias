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
    public class PeriodoEscolarRepositoryTests
    {
        private PeriodoEscolarRepository periodoEscolarRepository = new PeriodoEscolarRepository(new TutoriasContext());
        Periodo_Escolar periodoEscolar = new Periodo_Escolar()
        {
            FechaDeInicio = new DateTime(2023,2,1),
            FechaDeFin = new DateTime(2023,7,1)
        };
        [TestMethod()]
        public void AddPeriodoEscolarTest()
        {
            Assert.IsTrue(periodoEscolarRepository.AddPeriodoEscolar(periodoEscolar));
        }

        [TestMethod()]
        public void UpdatePeriodoEscolarTest()
        {
            Periodo_Escolar modPeriodo = periodoEscolarRepository.GetAllPeriodosEscolar().Last();
            modPeriodo.FechaDeInicio = DateTime.Now;
            Assert.IsTrue(periodoEscolarRepository.UpdatePeriodoEscolar(modPeriodo));
        }

        [TestMethod()]
        public void DeletePeriodoEscolarTest()
        {
            Periodo_Escolar delPeriodo = periodoEscolarRepository.GetAllPeriodosEscolar().Last();
            Assert.IsTrue(periodoEscolarRepository.DeletePeriodoEscolar(delPeriodo));
        }

        [TestMethod()]
        public void GetAllPeriodosEscolarTest()
        {
            List<Periodo_Escolar> result = periodoEscolarRepository.GetAllPeriodosEscolar();
            result.ForEach(x => Console.WriteLine(x.FechaDeInicio + " "+ x.FechaDeFin+"\n"));
            Assert.IsNotNull(result);
        }
    }
}