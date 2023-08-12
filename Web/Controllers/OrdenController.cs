using Antlr.Runtime.Misc;
using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    //evitar cambio
    public class OrdenController : Controller
    {
        // GET: Orden
        public ActionResult Index()
        {
            //Recibir Mensaje de notificación
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }

            //Carrito de Compras
            ViewBag.DetalleOrden = Carrito.Instancia.Items;

            if (Session["User"] != null)
            {
                ViewBag.IdDireccion = listaDirecciones();

                ViewBag.IdCuenta = listaCuentaPago();
            }
            return View();
        }

        private ActionResult DetalleCarrito()
        {
            return PartialView("_DetalleOrden", Carrito.Instancia.Items);
        }

        //Actualiza Cantidad 
        public ActionResult actualizarCantidad(int idProducto, int cantidad)
        {

            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidad(idProducto, cantidad);
            TempData.Keep();
            return PartialView("_DetalleOrden", Carrito.Instancia.Items);

        }

        //Ordena el producto 
        public ActionResult ordenProducto(int? idProducto)
        {
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem((int)idProducto);
            return PartialView("_OrdenCantidad");
        }

        //Actualiza 
        public ActionResult actualizarOrdenCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantProductos = Carrito.Instancia.Items.Count();
            return PartialView("_OrdenCantidad");

        }
        //Eliminar libro del carrito
        public ActionResult eliminarProducto(int? idProducto)
        {
            TempData["NotiCarrito"] = Carrito.Instancia.EliminarItem((int)idProducto);
            TempData.Keep();
            return PartialView("_DetalleOrden", Carrito.Instancia.Items);
        }

        //Admin Orden 
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexAdmin()
        {
            IEnumerable<Orden> lista = null;

            try
            {
                IServiceOrden _ServiceOrden = new ServiceOrden();
                lista = (IEnumerable<Orden>)_ServiceOrden.GetOrden();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Infraestructure.Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        // GET: Orden/Details/5
        public ActionResult Detalle(int? id)
        {
            IServiceOrden _ServiceOrden = new ServiceOrden();
            Orden orden = null;
            try
            {
                // Si va null

                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                orden = _ServiceOrden.GetOrdenById(id.Value);

                if (orden == null)
                {
                    TempData["Message"] = "No existe la orden solicitado";
                    TempData["Redirect"] = "Orden";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    //TempData.Keep();
                    return RedirectToAction("Default", "Error");
                }
                return View(orden);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Infraestructure.Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        private SelectList listaDirecciones(int? IdDireccion = 0)
        {
            Usuario oUsuario = (Usuario)Session["User"];

            IServiceDireccion _ServiceDirecc = new ServiceDireccion();
            IEnumerable<Direccion> listaDirecc = (IEnumerable<Direccion>)_ServiceDirecc.GetDireccionByIdUsuario(oUsuario.Id);

            return new SelectList(listaDirecc, "Id", "DireccionCompleta", IdDireccion);
        }

        private SelectList listaCuentaPago(int? IdCuenta = 0)
        {
            Usuario oUsuario = (Usuario)Session["User"];

            IServiceCuentaPago _Servicepago = new ServiceCuentaPago();
            IEnumerable<CuentaPago> listaDirecc = (IEnumerable<CuentaPago>)_Servicepago.GetCuentaByIdUsuario(oUsuario.Id);

            return new SelectList(listaDirecc, "Id", "CuentaEncrp", IdCuenta);
        }

        // POST: Orden/Create
        [HttpPost]
        public ActionResult Save(Orden orden, int? IdDireccion, int? IdCuenta)
        {
            Usuario oUsuario = (Usuario)Session["User"];
            var listaDetalle = Carrito.Instancia.Items;
            orden.SubTotal = Carrito.Instancia.GetTotal();
            orden.IdEstado = 5;

            IServiceProducto _ServiceProduct = new ServiceProducto();
            var product = _ServiceProduct.GetProductos();

            try
            {
                // Si no existe la sesión no hay nada
                if (Carrito.Instancia.Items.Count() <= 0)
                {
                    // Validaciones de datos requeridos.
                    TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Orden", "Debe seleccionar los productos que desea ordenar", SweetAlertMessageType.warning);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Remove("Estado_Orden");
                    ModelState.Remove("SubTotal");

                    if (ModelState.IsValid)
                    {

                        //Agregar cada línea de detalle a la orden
                        foreach (var item in listaDetalle)
                        {
                            var pro = product.FirstOrDefault(p => p.IdProducto ==
                             item.IdProducto);

                            if (pro != null && pro.Cantidad >= item.Cantidad)
                            {
                               //pro.Cantidad -= item.Cantidad;
                               //pro.VentasR += 1;

                               // Producto productEdit = _ServiceProduct.Save(pro, null);


                                OrdenDetalle ordenDetalle = new OrdenDetalle();
                                ordenDetalle.IdProducto = item.IdProducto;
                                ordenDetalle.Cantidad = item.Cantidad;
                                ordenDetalle.IdEstado = 5;
                                ordenDetalle.FechaEntrega = item.FechaEntrega;

                                orden.OrdenDetalle.Add(ordenDetalle);
                            }
                            else
                            {
                                //Cambiar el estado 
                                // Disponible -> Agotado
                                pro = product.FirstOrDefault(p => p.IdProducto ==
                                item.IdProducto);

                                pro.IdEstado = 4;
                                Producto productEdit = _ServiceProduct.Save(pro, null);


                                TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Cantidad Insuficiente", "Lo sentimos pero no contamos con suficientes artículos para su compra.", SweetAlertMessageType.error);
                                return RedirectToAction("Index");
                            }

                        }
                    }
                    //Guardar la orden
                    IServiceOrden _ServiceOrden = new ServiceOrden();
                    Orden ordenSave = _ServiceOrden.Save(orden);

                    foreach(var detalle  in ordenSave.OrdenDetalle)
                    {
                        var pro = product.FirstOrDefault(p => p.IdProducto == detalle.IdProducto);
                        pro.Cantidad -= detalle.Cantidad;
                        pro.VentasR += 1;

                         Producto productEdit = _ServiceProduct.Save(pro, null);

                    }

                    //Factura
                    Factura oFac = new Factura
                    {
                        IdUsuario = oUsuario.Id,
                        Fecha = (DateTime)ordenSave.FechaInicio,
                        IVA = 10,
                        Total = (orden.SubTotal * 0.10) + orden.SubTotal,
                        IdOrden = ordenSave.IdOrden,
                        IdDireccion = IdDireccion,
                        IdCuentaPago = IdCuenta
                    };

                    IServiceFactura _ServiceFactura = new ServiceFactura();
                    Factura factSave = _ServiceFactura.Save(oFac);

                    // Limpia el Carrito de compras
                    Carrito.Instancia.EliminarCarrito();
                    TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Orden", "Orden realizada correctamente", SweetAlertMessageType.success);
                    //return RedirectToAction("Detalle", "Factura", factSave.IdOrden);
                }

                // Reporte orden
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                // Salvar el error  
                Infraestructure.Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}
