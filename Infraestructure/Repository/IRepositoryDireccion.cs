using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryDireccion
    {
        IEnumerable<Direccion> GetDirecciones();
        Direccion GetDireccionByID(int Id);
        IEnumerable<Direccion> GetDireccionByIdUsuario(int IdUsuario);
        Direccion Save(Direccion direccion);
    }
}
