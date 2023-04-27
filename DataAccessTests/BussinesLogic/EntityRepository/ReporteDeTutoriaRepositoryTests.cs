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
    public class ReporteDeTutoriaRepositoryTests
    {
        private static TutoriasContext tutorias = new TutoriasContext();
        private ReporteDeTutoriaRepository reporteDeTutoriaRepository = new ReporteDeTutoriaRepository(tutorias);
        private static FechaDeTutoriaRepository fechaDeTutoriaRepository = new FechaDeTutoriaRepository(tutorias);
        private static TutorAcademicoRepository tutorAcademicoRepository = new TutorAcademicoRepository(tutorias);

        private Reporte_De_Tutoria reporte = new Reporte_De_Tutoria()
        {
            Comentarios = "Los alumnos no presentaron alguna queja en lo que llevan del periodo",
            FechaDeTutoria = fechaDeTutoriaRepository.GetFechaDeTutoriaActual(DateTime.Now),
            Fecha = DateTime.Now,
            TutorAcademico = tutorAcademicoRepository.GetTutorAcademico(1)
        };

        [TestMethod()]
        public void AddReporteDeTutoriaTest()
        {
            Assert.IsTrue(reporteDeTutoriaRepository.AddReporteDeTutoria(reporte));
        }

        [TestMethod()]
        public void GetReporteDeTutoriaTest()
        {
            Reporte_De_Tutoria result = reporteDeTutoriaRepository.GetReporteDeTutoria(
                tutorAcademicoRepository.GetTutorAcademico(1),
                fechaDeTutoriaRepository.GetFechaDeTutoriaActual(DateTime.Now));
            Console.WriteLine(result.Comentarios);
            Assert.IsNotNull(result);
        }
    }
}