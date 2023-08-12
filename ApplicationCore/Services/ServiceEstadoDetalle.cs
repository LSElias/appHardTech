using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEstadoDetalle : IServiceEstadoDetalle
    {
        public IEnumerable<Estado_Detalle> GetEstado()
        {
            IRepositoryEstadoDetalle oRep = new RepositoryEstadoDetalle();
            return oRep.GetEstado();
        }

        public Estado_Detalle GetEstadoByID(int Id)
        {
            IRepositoryEstadoDetalle oRep = new RepositoryEstadoDetalle();
            return oRep.GetEstadoByID(Id);
        }
    }
}
