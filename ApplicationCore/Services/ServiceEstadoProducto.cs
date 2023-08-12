using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEstadoProducto : IServiceEstadoProducto
    {
        public IEnumerable<Estado_Producto> GetEstado()
        {
            IRepositoryEstadoProducto oRep = new RepositoryEstadoProducto();
            return oRep.GetEstado();
        }

        public Estado_Producto GetEstadoByID(int Id)
        {
            IRepositoryEstadoProducto oRep = new RepositoryEstadoProducto();
            return oRep.GetEstadoByID(Id);
        }
    }
}
