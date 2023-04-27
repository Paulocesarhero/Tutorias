using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Tutorias.Service.DatabaseContext;

namespace DataAccess.BussinesLogic.Interface
{
    internal interface IUsuarioRepository
    {
        bool AddUser(Usuario usuario);
        Usuario Login(string username, string password);

    }
}
