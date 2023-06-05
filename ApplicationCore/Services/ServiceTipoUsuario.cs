using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTipoUsuario : IServiceTipoUsuario
    {
        public IEnumerable<TipoUsuario> GetTipoUsuario()
        {
           IRepositoryTipoUsuario oRep = new RepositoryTipoUsuario();
            return oRep.GetTipoUsuario();
        }

        public TipoUsuario GetTipoUsuarioByID(int Id)
        {
            IRepositoryTipoUsuario oRep = new RepositoryTipoUsuario();
            return oRep.GetTipoUsuarioByID(Id);
        }
    }
}
