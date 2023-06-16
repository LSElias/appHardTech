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
        IEnumerable<Factura> GetFacturaByIdUsuario(int IdUsuario);

        Factura GetFacturaById(int IdFactura);
        Factura GetFacturaByIdOrden(int IdOrden);
        Factura Save(Factura factura);
    }
}
