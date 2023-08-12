using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEstadoDetalle
    {
        IEnumerable<Estado_Detalle> GetEstado();
        Estado_Detalle GetEstadoByID(int Id);
    }
}
