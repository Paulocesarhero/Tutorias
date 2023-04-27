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
    public class UsuarioRepositoryTests
    {
        private static TutoriasContext tutoriasContext = new TutoriasContext();
        private UsuarioRepository usuarioRepository = new UsuarioRepository(tutoriasContext);

        private Usuario usuario = new Usuario()
        {
            Username = "Lolopol",
            Password = "MementoMory",
            ProgramaEducativo = new Programa_Educativo("Ingenieria de software"),
            TipoUsuario = new TipoUsuario()
            {
                Tipo = "Tutor academico"
            }
        };
        [TestMethod()]
        public void AddUserTest()
        {
            Assert.IsTrue(usuarioRepository.AddUser(usuario));
        }
        [TestMethod()]
        public void LoginTest()
        {
            Usuario usuarioResult = usuarioRepository.Login(usuario.Username, usuario.Password);
            Console.WriteLine(usuarioResult.Username + usuario.ProgramaEducativo.ProgramaEducativo);
            Assert.IsNotNull(usuarioResult);
        }


    }
}