using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public Producto GetByIdCategoria(int IdCategoria)
        {
            IRepositoryProducto oRep = new RepositoryProducto();
            return oRep.GetByIdCategoria(IdCategoria);
        }

        public Producto GetByIdProveedor(int IdProveedor)
        {
            IRepositoryProducto oRep = new RepositoryProducto();
            return oRep.GetByIdProveedor(IdProveedor);
        }

        public Producto GetProductoById(int Id)
        {
            IRepositoryProducto oRep = new RepositoryProducto();
            return oRep.GetProductoById(Id);
        }

        public IEnumerable<Producto> GetProductos()
        {
            IRepositoryProducto oRep = new RepositoryProducto();
            return oRep.GetProductos();
        }
    }
}
