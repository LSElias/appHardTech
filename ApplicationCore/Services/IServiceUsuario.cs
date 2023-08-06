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
        Usuario GetUsuario(string oCorreo, string oClave);
        Usuario GetUsuarioByID(int Id);
        IEnumerable<Usuario> GetUsuarioByIDTipoUsuario(int IdTipoUsuario);
        IEnumerable<Usuario> GetUsuarioByEstado(int IdEstado);
        Usuario GetUsuarioByEmail(string correo);
        Usuario Save(Usuario usuario, string[] selectedTipoUsuario, int[] arrayDirecciones, int[] arrayCuentas);

    }
}
