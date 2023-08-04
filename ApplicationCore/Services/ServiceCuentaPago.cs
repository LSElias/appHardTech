using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCuentaPago : IServiceCuentaPago
    {
        public IEnumerable<CuentaPago> GetByTipoPago(int IdTipoPago)
        {
            IRepositoryCuentaPago oRep = new RepositoryCuentaPago();
            return oRep.GetByTipoPago(IdTipoPago);
        }

        public IEnumerable<CuentaPago> GetCuentaByIdUsuario(int IdUsuario)
        {
            IRepositoryCuentaPago oRep = new RepositoryCuentaPago();
            return oRep.GetCuentaByIdUsuario(IdUsuario);
        }

        public CuentaPago GetCuentaPagoByID(int Id)
        {
            IRepositoryCuentaPago oRep = new RepositoryCuentaPago();
            return oRep.GetCuentaPagoByID(Id);
        }

        public IEnumerable<CuentaPago> GetCuentaPagos()
        {
            IRepositoryCuentaPago oRep = new RepositoryCuentaPago();
            return oRep.GetCuentaPagos();
        }

        public CuentaPago Save(CuentaPago cuenta)
        {
            IRepositoryCuentaPago oRep = new RepositoryCuentaPago();
            return oRep.Save(cuenta);
        }
    }
}
