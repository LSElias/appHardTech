using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceDireccion : IServiceDireccion
    {
        public Direccion GetDireccionByID(int Id)
        {
           IRepositoryDireccion oRep = new RepositoryDireccion();
            return oRep.GetDireccionByID(Id);        
        }

        public Direccion GetDireccionByIdUsuario(int IdUsuario)
        {
            IRepositoryDireccion oRep = new RepositoryDireccion();
            return oRep.GetDireccionByIdUsuario(IdUsuario);
        }

        public IEnumerable<Direccion> GetDirecciones()
        {
            IRepositoryDireccion oRep = new RepositoryDireccion();
            return oRep.GetDirecciones();
        }
    }
}
