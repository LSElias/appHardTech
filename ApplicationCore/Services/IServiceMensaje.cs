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
        Mensaje GetMensajeByIdProducto(int idProducto);
        Mensaje GetMensajeByIdUsuario(int IdUsuario);

    }
}
