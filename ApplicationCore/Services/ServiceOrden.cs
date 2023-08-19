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

        public void GetOrdenByDia(out string etiquetas1, out string valores1)
        {
           IRepositoryOrden repository = new RepositoryOrden();

            repository.GetOrdenByDia(out string etiquetas,out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores; 
        }

        public Orden GetOrdenById(int Id)
        {
            IRepositoryOrden oRep = new RepositoryOrden();
            return oRep.GetOrdenById(Id);
        }

        public void GetProductosTop(out string etiquetas1, out string valores1)
        {
            IRepositoryOrden repository = new RepositoryOrden();

            repository.GetProductosTop(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }

        public void GetVendedoresTop(out string etiquetas1, out string valores1)
        {
            IRepositoryOrden repository = new RepositoryOrden();

            repository.GetVendedoresTop(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }

        public Orden Save(Orden orden)
        {
            IRepositoryOrden oRep = new RepositoryOrden();
            return oRep.Save(orden);
        }
    }
}
