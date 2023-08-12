using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEstadoOrden : IServiceEstadoOrden
    {
        public IEnumerable<Estado_Orden> GetEstado()
        {
            IRepositoryEstadoOrden oRep = new RepositoryEstadoOrden();
            return oRep.GetEstado();
        }

        public Estado_Orden GetEstadoByID(int Id)
        {
            IRepositoryEstadoOrden oRep = new RepositoryEstadoOrden();
            return oRep.GetEstadoByID(Id);
        }
    }
}
