using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceCuentaPago
    {
        IEnumerable<CuentaPago> GetCuentaPagos();
        CuentaPago GetCuentaPagoByID(int Id);
    }
}
