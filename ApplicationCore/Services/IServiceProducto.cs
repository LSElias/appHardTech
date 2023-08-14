using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationCore.Services
{
    public interface IServiceProducto
    {
        IEnumerable<Producto> GetProductos();
        Producto GetProductoById(int Id);
        IEnumerable<Producto> GetByIdProveedor(int IdProveedor);
        IEnumerable<Producto> GetByIdCategoria(int IdCategoria);
        //Producto General
        IEnumerable<Producto> GetByNombre(String Nombre);

        //Lista de los nombre 
        IEnumerable<string> GetProductosNombre();

        Producto Save(Producto producto, IEnumerable<HttpPostedFileBase> images);

    }
}
