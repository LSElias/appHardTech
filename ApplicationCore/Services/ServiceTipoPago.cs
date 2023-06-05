using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTipoPago : IServiceTipoPago
    {
        public IEnumerable<TipoPago> GetTipoPago()
        {
            IRepositoryTipoPago oRep = new RepositoryTipoPago();
            return oRep.GetTipoPago();
        }

        public TipoPago GetTipoPagoByID(int Id)
        {
            IRepositoryTipoPago oRep = new RepositoryTipoPago();
            return oRep.GetTipoPagoByID(Id);
        }
    }
}
