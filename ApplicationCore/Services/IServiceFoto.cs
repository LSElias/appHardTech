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
        Foto GetFotoById(int Id);
        Foto GetFotoByIdProducto(int IdProducto);
    }
}
