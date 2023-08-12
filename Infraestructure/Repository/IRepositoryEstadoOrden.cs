using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryEstadoOrden
    {
        IEnumerable<Estado_Orden> GetEstado();
        Estado_Orden GetEstadoByID(int Id);
    }
}
