using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryFactura
    {
        IEnumerable<Factura> GetFactura();
        Factura GetFacturaById(int IdFactura);
        Factura GetFacturaByIdUsuario(int IdUsuario);
        Factura GetFacturaByIdOrden(int IdOrden);
    }
}
