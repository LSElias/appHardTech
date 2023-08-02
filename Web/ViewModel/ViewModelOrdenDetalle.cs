using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
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

        //Method SubTot
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

        /*Traer el ObjetoProducto*/
        public ViewModelOrdenDetalle(int IdProducto)
        {
            IServiceProducto _ServiceProduct = new ServiceProducto();
            this.IdProducto = IdProducto;
            this.Producto = _ServiceProduct.GetProductoById(this.IdProducto);
        }

    }

}