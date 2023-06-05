using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceOrdenDetalle
    {
        IEnumerable<OrdenDetalle> GetOrdenDetalle();
        OrdenDetalle GetOrdenDetByIdOrden(int IdOrden);
        OrdenDetalle GetOrdenDetByIdProducto(int idProducto);

    }
}
