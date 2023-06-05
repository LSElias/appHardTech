using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryUsuario
    {
        IEnumerable<Usuario> GetUsuario();
        Usuario GetUsuarioByID(int Id);
        Usuario GetUsuarioByIDTipoUsuario(int IdTipoUsuario);
        Usuario GetUsuarioByEstado(int Estado);
    }
}
