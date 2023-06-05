using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProductos();
        Producto GetProductoById(int Id);
        Producto GetByIdProveedor(int IdProveedor);
        Producto GetByIdCategoria(int IdCategoria);

    }
}
