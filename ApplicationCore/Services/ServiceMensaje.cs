using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMensaje : IServiceMensaje
    {
        public IEnumerable<Mensaje> GetMensaje()
        {
            IRepositoryMensaje oRep = new RepositoryMensaje();
            return oRep.GetMensaje();
        }

        public Mensaje GetMensajeById(int Id)
        {
            IRepositoryMensaje oRep = new RepositoryMensaje();
            return oRep.GetMensajeById(Id);
        }

        public Mensaje GetMensajeByIdProducto(int idProducto)
        {
            IRepositoryMensaje oRep = new RepositoryMensaje();
            return oRep.GetMensajeByIdProducto(idProducto);
        }

        public Mensaje GetMensajeByIdUsuario(int IdUsuario)
        {
            IRepositoryMensaje oRep = new RepositoryMensaje();
            return oRep.GetMensajeByIdUsuario(IdUsuario);
        }
    }
}
