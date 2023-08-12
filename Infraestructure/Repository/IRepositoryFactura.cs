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
        IEnumerable<Factura> GetFacturaByIdUsuario(int IdUsuario);
        Factura GetFacturaById(int IdFactura);

        Factura GetFacturaSimpleByIdOrden(int IdOrden);

        Factura GetFacturaByIdOrden(int IdOrden);
        Factura Save(Factura factura);
    }
}
