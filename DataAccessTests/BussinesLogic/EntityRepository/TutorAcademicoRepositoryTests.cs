using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.BussinesLogic.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_De_Tutorias.Utility;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.EntityRepository.Tests
{
    [TestClass()]
    public class TutorAcademicoRepositoryTests
    {
        private TutorAcademicoRepository tutorAcademicoRepository = new TutorAcademicoRepository(new TutoriasContext());
        Tutor_Academico tutorAcademico = new Tutor_Academico()
        {
            Nombres = "JUAN",
            Apellidos = "LOPEZ  VEGA",
            Usuario = new Usuario()
            {
                Username = "Juanli",
                Password = Security.HashSHA256("lolopol"),
                ProgramaEducativo = new Programa_Educativo("Ingenieria de software"),
                TipoUsuario = new TipoUsuario()
                {
                    Tipo = "Tutor academico"
                }
            }
        };

        [TestMethod()]
        [Priority(1)]
        public void AddTutorAcademicoTest()
        {
            Assert.IsTrue(tutorAcademicoRepository.AddTutorAcademico(tutorAcademico));
        }



        [TestMethod()]
        public void UpdateTutorAcademicoTest()
        {
            Tutor_Academico tutorAcademico = tutorAcademicoRepository.GetAllTutorAcademico().Last();
            tutorAcademico.Nombres = "Nombre modificado de prueba";
            Assert.IsNotNull(tutorAcademicoRepository.UpdateTutorAcademico(tutorAcademico));
        }

        [TestMethod()]
        public void GetTutorAcademicoTest()
        {

            Assert.IsNotNull(tutorAcademicoRepository.GetTutorAcademico(1));
        }

        [TestMethod()]
        public void GetAllTutorAcademicoTest()
        {
            List<Tutor_Academico> result = tutorAcademicoRepository.GetAllTutorAcademico();
            result.ForEach(x => Console.WriteLine(x.Nombres + "\n"));
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void DeleteTutorAcademicoTest()
        {
            Tutor_Academico tutorAcademico = tutorAcademicoRepository.GetAllTutorAcademico().Last();
            Assert.IsTrue(tutorAcademicoRepository.DeleteTutorAcademico(tutorAcademico));

        }

        [TestMethod()]
        public void GetTutorAcademicoByUserTest()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository(new TutoriasContext());
            Usuario usuario = usuarioRepository.GetAllUser().Last();
            Tutor_Academico result = tutorAcademicoRepository.GetTutorAcademicoByUser(usuario);
            Assert.IsNotNull(result);
        }
    }
}