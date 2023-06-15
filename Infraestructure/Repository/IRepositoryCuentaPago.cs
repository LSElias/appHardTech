using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryCuentaPago
    {
        IEnumerable<CuentaPago> GetCuentaPagos();
        IEnumerable<CuentaPago> GetByTipoPago(int IdTipoPago);

        CuentaPago GetCuentaPagoByID(int Id);
        CuentaPago Save(CuentaPago cuenta);
    }
}
