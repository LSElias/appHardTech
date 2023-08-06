using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceOrden : IServiceOrden
    {
        public IEnumerable<Factura> GetOrden()
        {
            IRepositoryOrden oRep = new RepositoryOrden();
            return oRep.GetOrden();
        }

        public Factura GetOrdenById(int Id)
        {
            IRepositoryOrden oRep = new RepositoryOrden();
            return oRep.GetOrdenById(Id);
        }

        public Factura Save(Factura orden)
        {
            IRepositoryOrden oRep = new RepositoryOrden();
            return oRep.Save(orden);
        }
    }
}
