﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryRespuesta
    {
        IEnumerable<Respuesta> GetRespuesta();
        IEnumerable<Respuesta> GetRespuestaByIdProveedor(int IdProveedor);

        Respuesta GetRespuestaById(int Id);
        Respuesta GetRespuestaByIdMensaje(int IdMensaje);
        Respuesta Save(Respuesta respuesta);
    }
}
  