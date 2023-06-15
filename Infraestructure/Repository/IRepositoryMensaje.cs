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
        IEnumerable<Mensaje> GetMensajeByIdProducto(int idProducto);
        IEnumerable<Mensaje> GetMensajeByIdUsuario(int IdUsuario);
        Mensaje GetMensajeById(int Id);
        Mensaje Save(Mensaje mensaje);
    }
}
