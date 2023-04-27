using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BussinesLogic.Interface;
using Microsoft.Identity.Client;
using Tutorias.Service.DatabaseContext;
using System.Data.Common;
using Sistema_De_Tutorias.Utility;

namespace DataAccess.BussinesLogic.EntityRepository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbContext _context;

        public UsuarioRepository(DbContext context)
        {
            _context = context;
        }

        public bool AddUser(Usuario usuario)
        {
            try
            {
                Usuario exist =
                    _context.Set<Usuario>().FirstOrDefault(
                        x => x.Username == usuario.Username);
                if (exist != null)
                {
                    return true;
                }

                Usuario usuarioToadd = new Usuario()
                {
                    Username = usuario.Username,
                    Password = Security.HashSHA256(usuario.Password),
                    ProgramaEducativo = _context.Set<Programa_Educativo>().FirstOrDefault(x =>
                        x.ProgramaEducativo == usuario.ProgramaEducativo.ProgramaEducativo),
                    TipoUsuario = _context.Set<TipoUsuario>().FirstOrDefault(x => x.Tipo == usuario.TipoUsuario.Tipo)
                };

                _context.Set<Usuario>().Add(usuarioToadd);
                return _context.SaveChanges() > 0;
            }
            catch (DbException e)
            {
                throw new Exception("Error al agregar un nuevo usuario", e);
            }
        }

        public Usuario Login(string username, string password)
        {
            try
            {
                return _context.Set<Usuario>()
                    .Where(x => x.Username == username
                                && x.Password == Security.HashSHA256(password))
                    .Include(c => c.TipoUsuario)
                    .Include(c => c.ProgramaEducativo)
                    .FirstOrDefault();
            }
            catch (DbException e)
            {
                throw new Exception("Error al agregar un nuevo usuario", e);
            }
        }
    }
}
