
using ApplicationCore.Services;
using Infraestructure.Models;
using log4net.Util.TypeConverters;
using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;
using Web.Security;
using Web.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProductos();
                ViewBag.title = "Productos";
                // Lista Categoría
                IServiceCategoria _ServiceCategoria = new ServiceCategoria();
                ViewBag.listaCategorias = _ServiceCategoria.GetCategorias();
                return View(lista);

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        public PartialViewResult ProductosxCat(int? id)
        {
            //contenido a actualizar
            IEnumerable<Producto> lista = null;
            IServiceProducto _ServiceProducto = new ServiceProducto();
            if (id != null && id > 0)
            {
                lista = _ServiceProducto.GetByIdCategoria((int)id);
            }
            else
            {
                lista = _ServiceProducto.GetProductos();
            }
            //Nombre vista, datos para la vista
            return PartialView("_PartialViewProducto", lista);
        }

        // GET: Producto
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexAdmin()
        {
            return View();
        }

        public ActionResult LoadData()
        {
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
                    var productoData = (from p in _context.Producto
                                        join c in _context.Categoria on p.IdCategoria equals c.Id
                                        join e in _context.Estado on p.IdEstado equals e.Id
                                        select new { p.IdProducto, Estado = e.Nombre , p.Precio, p.Nombre, Categoria = c.Nombre});

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
            catch (Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Proveedor)]
        public ActionResult IndexProveedor()
        {
            return View();
        }

        public ActionResult LoadDataProveedor()
        {
            var id = 0;
            if (Session["User"] != null)
            {
                Usuario oUsuario = (Usuario)Session["User"];
                if (oUsuario != null)
                {
                    id = oUsuario.Id;        
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
                    var productoData = (from p in _context.Producto where p.IdProveedor == id
                                        join c in _context.Categoria on p.IdCategoria equals c.Id
                                        join e in _context.Estado on p.IdEstado equals e.Id
                                        select new { p.IdProducto, Estado = e.Nombre, p.Precio, p.Nombre, Categoria = c.Nombre });

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


        public PartialViewResult SaveMensaje( int? id, string txt)
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


            MemoryStream target = new MemoryStream();
            IServiceMensaje oServiceMensaje = new ServiceMensaje();
            Mensaje oMensaje = new Mensaje();
            oMensaje.IdProducto = id;
            oMensaje.IdUsuario = _idUsuario;
            oMensaje.Mensaje1 = txt;

            if (txt == "" || txt == null )
            {
                TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Error", "El mensaje no puede estar vacío", SweetAlertMessageType.warning);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Mensaje oMensajeI = oServiceMensaje.Save(oMensaje);
 
                }

            }
            IServiceProducto oServiceProduct = new ServiceProducto();
            Producto oPrduct = oServiceProduct.GetProductoById((int)id);

            return PartialView("_ParticialViewMsj", oPrduct);

        }


        public PartialViewResult SaveRespuesta(int? id, int? objProd,string txtResp)
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

            MemoryStream target = new MemoryStream();
            IServiceRespuesta oServiceRespuesta = new ServiceRespuesta();

            Respuesta oRespuesta = new Respuesta();
            oRespuesta.IdProveedor = _idUsuario; //Con el session validar que solo Prov. responda
            oRespuesta.IdMensaje = id;
            oRespuesta.Respuesta1 = txtResp;

            if (txtResp == "" || txtResp == null)
            {
                TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Error", "La respuesta no puede estar vacía", SweetAlertMessageType.warning);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Respuesta oRespI = oServiceRespuesta.Save(oRespuesta);
                }
            }

            IServiceProducto oServiceProduct = new ServiceProducto();
            Producto oPrduct = oServiceProduct.GetProductoById((int)objProd);

            return PartialView("_ParticialViewMsj", oPrduct);
            
        }

        public ActionResult EliminarFoto(int? id, string txt, int? idProveedor)
        {

            IServiceFoto oServiceFoto = new ServiceFoto();
            Foto oFoto= oServiceFoto.GetFotoById((int)id);

            Producto oPrduct = oServiceFoto.Eliminar(oFoto);

            return PartialView("_PartialViewFotosEdit", oPrduct);

        }

        public ActionResult Detalle(int? id)

        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto= null;
            try
            {
                //Si es null el parametro
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                producto= _ServiceProducto.GetProductoById(Convert.ToInt32(id));
                if (producto== null)
                {
                    TempData["Message"] = "No existe el producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "Index";
                    //Redireccion a la vista del error
                    return RedirectToAction("Default", "Error");

                }
                return View(producto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }

        }

        [CustomAuthorize((int)Roles.Proveedor, (int)Roles.Administrador)]
        public ActionResult Crear()
        {
            //Recursos que necesito para crear un Libro
            //Listado de autores
            //Listado de categorías
            ViewBag.IdCategoria = ListaCategorias();
            ViewBag.IdEstado = ListaEstados();

            return View();
        }

        [CustomAuthorize((int)Roles.Proveedor, (int)Roles.Administrador)]
        public ActionResult Editar(int? id)
        {
            IServiceProducto _Service = new ServiceProducto();
            Producto formato = null;
            try
            {
                //Si es null el parametro
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                formato = _Service.GetProductoById(Convert.ToInt32(id));
                if (formato == null)
                {
                    TempData["Message"] = "No existe el producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "IndexProveedor";
                    //Redireccion a la vista del error
                    return RedirectToAction("Default", "Error");

                }
                ViewBag.IdCategoria = ListaCategorias((int)formato.IdCategoria);
                ViewBag.IdEstado = ListaEstados((int)formato.IdEstado);
                return View(formato);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }


        private SelectList ListaCategorias(int idCategoria = 0)
        {
            IServiceCategoria _service = new ServiceCategoria();
            IEnumerable<Categoria> lista = _service.GetCategorias();
            return new SelectList(lista, "Id", "Nombre", idCategoria);
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

        // POST: Libro/Create-Update
        [HttpPost]
        public ActionResult Save(Producto producto, IEnumerable<HttpPostedFileBase> images, List<Foto> lista)
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

            var imageList = new List<Foto>();
            MemoryStream target = new MemoryStream();
            IServiceProducto _Service = new ServiceProducto();
            IServiceFoto _ServiceFoto = new ServiceFoto();
            producto.IdProveedor = _idUsuario; 
            try
            {
                ModelState.Remove("VentasR");
                if (ModelState.IsValid)
                {
                    Producto oProductoI = _Service.Save(producto, images);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    //Recurso a cargar en la vista

                    //Debe funcionar para crear y modificar
                    ViewBag.IdCategoria = ListaCategorias();
                    ViewBag.IdEstado = ListaEstados();
                    return View("Crear", producto);
                }

                return RedirectToAction("IndexProveedor");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexProveedor";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

    }
}