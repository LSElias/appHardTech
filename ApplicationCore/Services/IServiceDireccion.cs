using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceDireccion
    {
        IEnumerable<Direccion> GetDirecciones();
        Direccion GetDireccionByID(int Id);
        Direccion GetDireccionByIdUsuario(int IdUsuario);
        Direccion Save(Direccion direccion);

    }
}
