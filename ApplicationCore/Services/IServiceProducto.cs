using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceProducto
    {
        IEnumerable<Producto> GetProductos();
        Producto GetProductoById(int Id);
        IEnumerable<Producto> GetByIdProveedor(int IdProveedor);
        IEnumerable<Producto> GetByIdCategoria(int IdCategoria);
        IEnumerable<Producto> GetByNombre(String Nombre);

        Producto Save(Producto producto);

    }
}
