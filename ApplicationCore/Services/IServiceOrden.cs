using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceOrden
    {
        IEnumerable<Factura> GetOrden();
        Factura GetOrdenById(int Id);
        Factura Save(Factura orden);

    }
}
