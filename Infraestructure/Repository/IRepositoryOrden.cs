using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryOrden
    {
        IEnumerable<Orden> GetOrden();
        Orden GetOrdenById(int Id);
        Orden Save(Orden orden);
    }
}
