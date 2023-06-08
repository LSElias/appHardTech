using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceFactura
    {
        IEnumerable<Factura> GetFactura();
        Factura GetFacturaById(int IdFactura);
        IEnumerable<Factura> GetFacturaByIdUsuario(int IdUsuario);
        Factura GetFacturaByIdOrden(int IdOrden);
    }
}
