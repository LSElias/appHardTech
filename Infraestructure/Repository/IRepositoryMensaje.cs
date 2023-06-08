using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryMensaje
    {
        IEnumerable<Mensaje> GetMensaje();
        Mensaje GetMensajeById(int Id);
        IEnumerable<Mensaje> GetMensajeByIdProducto(int idProducto);
        IEnumerable<Mensaje> GetMensajeByIdUsuario(int IdUsuario);

    }
}
