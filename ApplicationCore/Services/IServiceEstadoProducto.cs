using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEstadoProducto
    {
        IEnumerable<Estado_Producto> GetEstado();
        Estado_Producto GetEstadoByID(int Id);
    }
}
