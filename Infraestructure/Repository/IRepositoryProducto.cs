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
        IEnumerable<Producto> GetByIdProveedor(int IdProveedor);
        IEnumerable<Producto> GetByIdCategoria(int IdCategoria);
        Producto Save(Producto producto);

    }
}
