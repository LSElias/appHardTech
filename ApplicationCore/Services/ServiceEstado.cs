using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEstado : IServiceEstado
    {
        public IEnumerable<Estado> GetEstado()
        {
            IRepositoryEstado oRep = new RepositoryEstado();
            return oRep.GetEstado();
        }

        public Estado GetEstadoByID(int Id)
        {
            IRepositoryEstado oRep = new RepositoryEstado();
            return oRep.GetEstadoByID(Id);
        }
    }
}
