using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceFoto : IServiceFoto
    {
        public Producto Eliminar(Foto foto)
        {
            IRepositoryFoto oRep = new RepositoryFoto();
            return oRep.Eliminar(foto);
        }

        public IEnumerable<Foto> GetFoto()
        {
           IRepositoryFoto oRep = new RepositoryFoto();
           return oRep.GetFoto();
        }

        public Foto GetFotoById(int Id)
        {
            IRepositoryFoto oRep = new RepositoryFoto();
            return oRep.GetFotoById(Id);
        }

        public IEnumerable<Foto> GetFotoByIdProducto(int IdProducto)
        {
            IRepositoryFoto oRep = new RepositoryFoto();
            return oRep.GetFotoByIdProducto(IdProducto);
        }

        public Foto Save(Foto foto)
        {
            IRepositoryFoto oRep = new RepositoryFoto();
            return oRep.Save(foto);

        }
    }
}
