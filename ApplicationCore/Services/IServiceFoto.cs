using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceFoto
    {
        IEnumerable<Foto> GetFoto();
        IEnumerable<Foto> GetFotoByIdProducto(int IdProducto);
        Foto GetFotoById(int Id);
        Foto Save(Foto foto);

    }
}
