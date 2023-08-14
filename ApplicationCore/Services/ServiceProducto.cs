using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public IEnumerable<Producto> GetByIdCategoria(int IdCategoria)
        {
            IRepositoryProducto oRep = new RepositoryProducto();
            return oRep.GetByIdCategoria(IdCategoria);
        }

        public IEnumerable<Producto> GetByIdProveedor(int IdProveedor)
        {
            IRepositoryProducto oRep = new RepositoryProducto();
            return oRep.GetByIdProveedor(IdProveedor);
        }

        public IEnumerable<Producto> GetByNombre(string Nombre)
        {
            IRepositoryProducto oRep = new RepositoryProducto();
            return oRep.GetByNombre(Nombre);
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

        public IEnumerable<string> GetProductosNombre()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductos().Select(x => x.Nombre);
        }

        public Producto Save(Producto producto, IEnumerable<HttpPostedFileBase> images)
        {
            IRepositoryProducto oRep = new RepositoryProducto();
            return oRep.Save(producto, images);
        }
    }
}
