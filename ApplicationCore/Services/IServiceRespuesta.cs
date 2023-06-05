using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceRespuesta
    {
        IEnumerable<Respuesta> GetRespuesta();
        Respuesta GetRespuestaById(int Id);
        Respuesta GetRespuestaByIdMensaje(int IdMensaje);
        Respuesta GetRespuestaByIdUsuario(int IdUsuario);
    }
}
