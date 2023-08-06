using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net.Util.TypeConverters;
using Web.Utils;
using ApplicationCore.Services;
using Web.Security;

namespace Web.Controllers
{
    public class FacturaController : Controller
    {
        // GET: Factura


        [CustomAuthorize((int)Roles.Cliente)]
        public ActionResult MisOrdenes()
        {
            return View();
        }

        public ActionResult LoadData()
        {
            var _idUsuario = 0;
            if (Session["User"] != null)
            {
                Usuario oUsuario = (Usuario)Session["User"];
                if (oUsuario != null)
                {
                    _idUsuario = oUsuario.Id;
                }
            }
            try
            {
                using (MyContext _context = new MyContext())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" +
                        Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                    // Paginación (tamaño)
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Obtención de datos
                    var productoData = (from f in _context.Factura
                                        from o in _context.Orden
                                        from u in _context.Usuario
                                        from e in _context.Estado

                                        where f.IdUsuario == _idUsuario
                                        where f.IdOrden == o.IdOrden
                                        where o.IdEstado == e.Id
                                        where f.IdUsuario == u.Id
                                        select new { Id = f.IdFactura,IdOrden = o.IdOrden, u.Nombre, f.Total, f.Fecha, Estado = e.Nombre }); ;
                    //   join o in _context.Orden on f.IdOrden equals o.Id
                    // join u in _context.Usuario on f.IdUsuario equals u.Id
                    //  join e in _context.Estado on o.Estado equals e.Id

                    // Organización
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        productoData = productoData.OrderBy(sortColumn + " " + sortColumnDir);
                    }
                    // Buscar
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        productoData = productoData.Where(m => m.Nombre.ToLower().Contains(searchValue.ToLower()));
                    }

                    // Numero total de columnas
                    recordsTotal = productoData.Count();
                    //Paginación
                    var data = productoData.Skip(skip).Take(pageSize).ToList();
                    // Retorno del JSON
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }
        
        [CustomAuthorize((int)Roles.Proveedor)]
        public ActionResult MisVentas()
        {
            return View();
        }

        public ActionResult LoadVentas()
        {
            var _idUsuario = 0;
            if (Session["User"] != null)
            {
                Usuario oUsuario = (Usuario)Session["User"];
                if (oUsuario != null)
                {
                    _idUsuario = oUsuario.Id;
                }
            }

            try
            {
                using (MyContext _context = new MyContext())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" +
                        Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                    // Paginación (tamaño)
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Obtención de datos
                    var productoData = (
                        
                                        from p in _context.Producto
                                        from o in _context.Orden
                                        from od in _context.OrdenDetalle
                                        from e in _context.Estado
                                        from f in _context.Factura

                                        where p.IdProveedor == _idUsuario
                                        where p.IdProducto == od.IdProducto
                                        where od.IdOrden == o.IdOrden
                                        where o.IdOrden == f.IdOrden
                                        where od.IdEstado == e.Id

                                        select new { Id = f.IdFactura, f.Fecha , Producto = p.Nombre , od.Cantidad, Estado = e.Nombre, o.IdOrden }); ;
                    //   join o in _context.Orden on f.IdOrden equals o.Id
                    // join u in _context.Usuario on f.IdUsuario equals u.Id
                    //  join e in _context.Estado on o.Estado equals e.Id

                    // Organización
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        productoData = productoData.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    // Numero total de columnas
                    recordsTotal = productoData.Count();
                    //Paginación
                    var data = productoData.Skip(skip).Take(pageSize).ToList();
                    // Retorno del JSON
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Factura/Details/5

        [CustomAuthorize((int)Roles.Cliente, (int)Roles.Proveedor, (int)Roles.Administrador)]
        public ActionResult Detalle(int? id)
        {

            IServiceFactura _ServiceFactura = new ServiceFactura();
            Factura factura = null;
            try
            {
                // Si va null

                if (id == null)
                {
                    return RedirectToAction("MisOrdenes", "Factura");
                }

                factura = _ServiceFactura.GetFacturaById((int)id);
                if (factura == null)
                {
                    TempData["Message"] = "No existe la orden solicitado";
                    TempData["Redirect"] = "Orden";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    //TempData.Keep();
                    return RedirectToAction("Default", "Error");
                }
                return View(factura);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        // GET: Factura/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Factura/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        private SelectList ListaEstados(int idEstado = 0)
        {
            IServiceEstado _service = new ServiceEstado();
            List<Estado> estado = new List<Estado>
            {
                _service.GetEstadoByID(3),
                _service.GetEstadoByID(4)
            };
            IEnumerable<Estado> lista = estado;
            return new SelectList(lista, "Id", "Nombre", idEstado);
        }


        public ActionResult Editar(int? id)
        {
            IServiceFactura _Service = new ServiceFactura();
            Factura formato = null;

            try
            {
                //Si es null el parametro
                if (id == null)
                {
                    return RedirectToAction("MisVentas", "Factura");
                }
                formato = _Service.GetOrdenById(Convert.ToInt32(id));

                if (formato == null)
                {
                    TempData["Message"] = "No existe la orden solicitada";
                    TempData["Redirect"] = "Factura";
                    TempData["Redirect-Action"] = "MisVentas";

                    return RedirectToAction("MisVentas", "Factura");
                }
                ViewBag.IdEstado = ListaEstados((int)formato.IdEstado);
                return View(formato);
            }
            catch (Exception ex)
            {
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }
    }
}
