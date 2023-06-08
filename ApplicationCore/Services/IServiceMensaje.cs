using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceMensaje
    {
        IEnumerable<Mensaje> GetMensaje();
        Mensaje GetMensajeById(int Id);
        IEnumerable<Mensaje> GetMensajeByIdProducto(int idProducto);
        IEnumerable<Mensaje> GetMensajeByIdUsuario(int IdUsuario);

    }
}
