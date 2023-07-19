using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infraestructure.Repository
{
    public interface IRepositoryUsuario
    {
        Usuario GetUsuario(string oCorreo, string oClave);
        Usuario GetUsuarioByID(int Id);
        IEnumerable<Usuario> GetUsuarioByIDTipoUsuario(int IdTipoUsuario);
        IEnumerable<Usuario> GetUsuarioByEstado(int IdEstado);
        Usuario Save(Usuario usuario, string[] selectedTipoUsuario);
    }
}
