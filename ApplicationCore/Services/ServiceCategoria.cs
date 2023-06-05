using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCategoria : IServiceCategoria
    {
        public Categoria GetCategoriaByID(int Id)
        {
           IRepositoryCategoria oRep = new RepositoryCategoria();
            return oRep.GetCategoriaByID(Id);
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            IRepositoryCategoria oRep = new RepositoryCategoria();
            return oRep.GetCategorias();
        }
    }
}
