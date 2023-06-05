using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceOrdenDetalle : IServiceOrdenDetalle
    {
        public IEnumerable<OrdenDetalle> GetOrdenDetalle()
        {
           IRepositoryOrdenDetalle oRep = new RepositoryOrdenDetalle();
            return oRep.GetOrdenDetalle();
        }

        public OrdenDetalle GetOrdenDetByIdOrden(int IdOrden)
        {
            IRepositoryOrdenDetalle oRep = new RepositoryOrdenDetalle();
            return oRep.GetOrdenDetByIdOrden(IdOrden);
        }

        public OrdenDetalle GetOrdenDetByIdProducto(int idProducto)
        {
            IRepositoryOrdenDetalle oRep = new RepositoryOrdenDetalle();
            return oRep.GetOrdenDetByIdProducto(idProducto);
        }
    }
}
