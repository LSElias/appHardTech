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
        Producto GetByIdProveedor(int IdProveedor);
        Producto GetByIdCategoria(int IdCategoria);
    }
}
