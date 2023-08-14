using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    //service cambio
    public class ServiceOrden : IServiceOrden
    {
        public IEnumerable<Orden> GetOrden()
        {
            IRepositoryOrden oRep = new RepositoryOrden();
            return oRep.GetOrden();
        }

        public void GetOrdenByDia(out string valores1, out string etiquetas1)
        {
           IRepositoryOrden repository = new RepositoryOrden();

            repository.GetOrdenByDia(out string valores, out string etiquetas);
            etiquetas1 = etiquetas;
            valores1 = valores; 
        }

        public Orden GetOrdenById(int Id)
        {
            IRepositoryOrden oRep = new RepositoryOrden();
            return oRep.GetOrdenById(Id);
        }

        public Orden Save(Orden orden)
        {
            IRepositoryOrden oRep = new RepositoryOrden();
            return oRep.Save(orden);
        }
    }
}
