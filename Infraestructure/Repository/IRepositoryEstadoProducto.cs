using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryEstadoProducto
    {
        IEnumerable<Estado_Producto> GetEstado();
        Estado_Producto GetEstadoByID(int Id);
    }
}
