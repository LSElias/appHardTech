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
        IEnumerable<Factura> GetFacturaByIdUsuario(int IdUsuario);
        Factura GetFacturaByIdOrden(int IdOrden);
    }
}
