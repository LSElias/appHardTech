using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.ViewModel;

namespace Web.Utils
{
    public class Carrito
    {
        public List<ViewModelOrdenDetalle> Items { get; private set; }

        public static readonly Carrito Instancia;

        static Carrito()
        {
            if (HttpContext.Current.Session["carrito"] == null)
            {
                Instancia = new Carrito();
                Instancia.Items = new List<ViewModelOrdenDetalle>();
                HttpContext.Current.Session["carrito"] = Instancia;
            }
            else
            {
                Instancia = (Carrito)HttpContext.Current.Session["carrito"];
            }
        }

        protected Carrito() { }

        public String AgregarItem(int productId)
        {
            String mensaje = "";

            ViewModelOrdenDetalle nuevoItem = new ViewModelOrdenDetalle(productId);


            if (nuevoItem != null)
            {
                if (Items.Exists(x => x.IdProducto == productId))
                {
                    ViewModelOrdenDetalle item = Items.Find(x => x.IdProducto == productId);
                    item.Cantidad++;
                }
                else
                {
                    nuevoItem.Cantidad = 1;
                    Items.Add(nuevoItem);
                }
                mensaje = SweetAlertHelper.Mensaje("Producto", "Dato agregado", SweetAlertMessageType.success);

            }
            else
            {
                mensaje = SweetAlertHelper.Mensaje("Producto", "El producto solicitado no existe", SweetAlertMessageType.warning);
            }
            return mensaje;
        }

        public String SetItemCantidad(int productId, int cantidad)
        {
            String mensaje = "";

            if (cantidad == 0)
            {
                EliminarItem(productId);
                mensaje = SweetAlertHelper.Mensaje("Producto", "Producto eliminado correctamente", SweetAlertMessageType.success);

            }
            else
            {
                // Encuentra el artículo y actualiza la Cantidad
                ViewModelOrdenDetalle actualizarItem = new ViewModelOrdenDetalle(productId);
                if (Items.Exists(x => x.IdProducto == productId))
                {
                    ViewModelOrdenDetalle item = Items.Find(x => x.IdProducto == productId);
                    item.Cantidad = cantidad;
                    mensaje = SweetAlertHelper.Mensaje("Producto", "La cantidad fue actualizada correctamente", SweetAlertMessageType.success);

                }
            }
            return mensaje;

        }

        public String EliminarItem(int productId)
        {
            String mensaje = "El producto lamentablemente no existe";
            if (Items.Exists(x => x.IdProducto == productId))
            {
                var itemEliminar = Items.Single(x => x.IdProducto == productId);
                Items.Remove(itemEliminar);
                mensaje = SweetAlertHelper.Mensaje("Producto", "Producto eliminado correctamente", SweetAlertMessageType.success);
            }
            return mensaje;

        }

        public double GetTotal()
        {
            double total = 0;
            total = Items.Sum(x => x.SubTot);

            return total;
        }

        public int GetCountItems()
        {
            int total = 0;
            total = Items.Sum(x => x.Cantidad);

            return total;
        }

        public void EliminarCarrito()
        {
            Items.Clear();

        }


    }
}