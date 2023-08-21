using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
 //evitar cambio
    public interface IServiceOrden
    {
        IEnumerable<Orden> GetOrden();
        Orden GetOrdenById(int Id);
        Orden Save(Orden orden);

        //Reporte
        //Reporte de las compras por semana
        void GetOrdenByDia(out string etiquetas, out string valores);

        //Top Productos
        void GetProductosTop(out string etiquetas, out string valores);

        //Top Vendedores
        void GetVendedoresTop(out string etiquetas, out string valores);

        //Top 3 Vendedores Deficientes
        void GetVendDeficiente(out string etiquetas, out string valores);

        //Producto Más Vendido
        void GetMasVendidos(out string etiquetas, out string valores, int IdUsuario);

        //Cantidad de evaluaciones por c. dato de escala 
        void GetEvaluacionXProveedor(out string etiquetas, out string valores, int IdUsuario = 0);
        void GetDestacadoCliente(out string etiquetasM, out string valoresM, int id = 0);
    }
}
