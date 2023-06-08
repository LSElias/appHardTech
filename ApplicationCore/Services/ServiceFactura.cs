using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceFactura : IServiceFactura
    {
        public IEnumerable<Factura> GetFactura()
        {
            IRepositoryFactura oRep = new RepositoryFactura();
            return oRep.GetFactura();
        }

        public Factura GetFacturaById(int IdFactura)
        {
            IRepositoryFactura oRep = new RepositoryFactura();
            return oRep.GetFacturaById(IdFactura);
        }

        public Factura GetFacturaByIdOrden(int IdOrden)
        {
            IRepositoryFactura oRep = new RepositoryFactura();
            return oRep.GetFacturaByIdOrden(IdOrden);
        }

        public IEnumerable<Factura> GetFacturaByIdUsuario(int IdUsuario)
        {
            IRepositoryFactura oRep = new RepositoryFactura();
            return oRep.GetFacturaByIdUsuario(IdUsuario);
        }
    }
}
