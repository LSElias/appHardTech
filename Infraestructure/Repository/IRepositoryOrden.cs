using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
//evitar cambio
    public interface IRepositoryOrden
    {
        IEnumerable<Orden> GetOrden();
        Orden GetOrdenById(int Id);
        Orden Save(Orden orden);

        //Reporte
        //Reporte de las compras por semana
        void GetOrdenByDia(out string etiquetas, out string valores);

        //Top Productos
        void GetProductosTop(out string etiquetas, out string valores);

    }
}
