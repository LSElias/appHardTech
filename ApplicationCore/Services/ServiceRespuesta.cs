using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRespuesta : IServiceRespuesta
    {
        public IEnumerable<Respuesta> GetRespuesta()
        {
            IRepositoryRespuesta oRep = new RepositoryRespuesta();
            return oRep.GetRespuesta();
        }

        public Respuesta GetRespuestaById(int Id)
        {
            IRepositoryRespuesta oRep = new RepositoryRespuesta();
            return oRep.GetRespuestaById(Id);
        }

        public Respuesta GetRespuestaByIdMensaje(int IdMensaje)
        {
            IRepositoryRespuesta oRep = new RepositoryRespuesta();
            return oRep.GetRespuestaByIdMensaje(IdMensaje);
        }

        public Respuesta GetRespuestaByIdUsuario(int IdUsuario)
        {
            IRepositoryRespuesta oRep = new RepositoryRespuesta();
            return oRep.GetRespuestaByIdUsuario(IdUsuario);
        }
    }
}
