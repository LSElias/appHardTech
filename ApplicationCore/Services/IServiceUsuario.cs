using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        IEnumerable<Usuario> GetUsuario();
        Usuario GetUsuarioByID(int Id);
        IEnumerable<Usuario> GetUsuarioByIDTipoUsuario(int IdTipoUsuario);
        IEnumerable<Usuario> GetUsuarioByEstado(int Estado);
        Usuario LogIn(string email, string clave);
        Usuario Save(Usuario usuario, string[] selectedTipoUsuario);

    }
}
