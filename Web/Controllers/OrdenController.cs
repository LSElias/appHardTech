﻿using Antlr.Runtime.Misc;
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
    public class OrdenController : Controller
    {
        // GET: Orden
        [CustomAuthorize((int)Roles.Cliente)]
        public ActionResult Index()
        {
            //Recibir Mensaje de notificación
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }

            //Carrito de Compras
            ViewBag.DetalleOrden = Carrito.Instancia.Items;

            ViewBag.IdDireccion = listaDirecciones(); 

            ViewBag.IdCuenta = listaCuentaPago();

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
                lista = _ServiceOrden.GetOrden();
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

        //Orden por proveedor 
        //Agregar en Service y Repository, buscar las orden por proveeedor

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
                    if (ModelState.IsValid)
                    {
                        
                        //Agregar cada línea de detalle a la orden
                        foreach (var item in listaDetalle)
                        {
                            OrdenDetalle ordenDetalle = new OrdenDetalle();
                            ordenDetalle.IdProducto = item.IdProducto;
                            ordenDetalle.Cantidad = item.Cantidad;
                            ordenDetalle.IdEstado = 5; 
                            
                           

                            orden.OrdenDetalle.Add(ordenDetalle);
                        }
                    }
                    //Guardar la orden
                    IServiceOrden _ServiceOrden = new ServiceOrden();
                    Orden ordenSave = _ServiceOrden.Save(orden);

                    //Factura
                    Factura oFac = new Factura
                    {
                        IdUsuario = oUsuario.Id,
                        Fecha = (DateTime)ordenSave.FechaInicio,
                        IVA = 10,
                        Total = (ordenSave.SubTotal * (10 / 100)) + ordenSave.SubTotal,
                        IdOrden = ordenSave.IdOrden,
                        IdDireccion = IdDireccion,
                        IdCuentaPago = IdCuenta
                    };

                    IServiceFactura _ServiceFactura = new ServiceFactura();
                    Factura factSave = _ServiceFactura.Save(oFac);

                    // Limpia el Carrito de compras
                    Carrito.Instancia.EliminarCarrito();
                    TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Orden", "Orden realizada correctamente", SweetAlertMessageType.success);
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

        // GET: Orden/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orden/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
