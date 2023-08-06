using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    //evitar cambio
    public class ViewModelOrdenDetalle
    {
        public int IdOrden { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int IdEstado { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }

        //Method SubTot Precio Acumulado
        public double SubTot
        {
            get { return calcSubTot(); }
        }
        public double calcSubTot()
        {
            return (double)(this.Producto.Precio * this.Cantidad);
        }

        //Method Precio
        public double ProdPrecio
        {
            get { return (double)Producto.Precio; }
        }

        public int direccion { get; set; }
        public int cuentaPago { get; set; }

        /*Traer el ObjetoProducto*/
        public ViewModelOrdenDetalle(int IdProducto)
        {
            IServiceProducto _ServiceProduct = new ServiceProducto();
            this.IdProducto = IdProducto;
            this.Producto = _ServiceProduct.GetProductoById(this.IdProducto);
        }

    }

}