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
        IEnumerable<Orden> GetOrden();
        Orden GetOrdenById(int Id);
    }
}
