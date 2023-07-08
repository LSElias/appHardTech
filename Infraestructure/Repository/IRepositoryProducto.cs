using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infraestructure.Repository
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProductos();
        Producto GetProductoById(int IdProducto);
        IEnumerable<Producto> GetByIdProveedor(int IdProveedor);
        IEnumerable<Producto> GetByIdCategoria(int IdCategoria);
        IEnumerable<Producto> GetByNombre(String Nombre);

        Producto Save(Producto producto, IEnumerable<HttpPostedFileBase> images);


    }
}
