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
    public class CatedraticoRepositoryTests
    {
        private static TutoriasContext tutoriasContext = new TutoriasContext();
        private CatedraticoRepository catedraticoRepository= new CatedraticoRepository(tutoriasContext); 
        private Catedratico catedratico = new Catedratico()
        {
            NombreCompleto = "ANITA LA HUERFANITA"
        };
        [TestMethod()]
        [Priority(1)]
        public void AddCatedraticoTest()
        {
            Assert.IsTrue(catedraticoRepository.AddCatedratico(catedratico));
        }
        
        [TestMethod()]
        [Priority(2)]
        public void GetByNameTest()
        {
            
            Assert.AreEqual(catedratico.NombreCompleto, catedraticoRepository.GetByName("ANITA LA HUERFANITA").NombreCompleto);
        }
        
        
        
        [TestMethod()]
        [Priority(3)]
        public void UpdateCatedraticoTest()
        {
            catedratico = catedraticoRepository.GetByName("ANITA LA HUERFANITA");
            catedratico.NombreCompleto = "ANITA LA HUERFANITA 1";
            Assert.IsTrue(catedraticoRepository.UpdateCatedratico(catedratico));
        }
        
        [TestMethod()]
        [Priority(4)]
        public void DeleteCatedraticoTest()
        {
            catedratico = catedraticoRepository.GetByName("ANITA LA HUERFANITA 1");
            Assert.IsTrue(catedraticoRepository.DeleteCatedratico(catedratico));
        }
        [TestMethod()]
        public void GetAllCatedraticosTest()
        {
            Assert.IsNotNull(catedraticoRepository.GetAllCatedraticosWithEE());
        }

    }
}