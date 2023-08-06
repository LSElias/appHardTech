using Antlr.Runtime.Tree;
using ApplicationCore.Services;
using Infraestructure.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;
using Web.Models;
using Web.Security;
using Web.Utils;
using Web.ViewModel;
using static log4net.Appender.RollingFileAppender;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadUsuario()
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
                    var productoData = (from u in _context.Usuario
                                        from e in _context.Estado

                                        where u.TipoUsuario.All(x => x.Id != 1)
                                        where u.IdEstado == e.Id
                                        
                                        select new { u.Id, Nombre = u.Nombre + " " + u.Apellido1 + " " + u.Apellido2, u.Correo, Estado = e.Nombre, eID = e.Id}


                                      
                                        );;
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

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult UsuariosDesactivados()
        {
            return View();
        }

        public ActionResult LoadDesactivados()
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
                    var productoData = (from u in _context.Usuario
                                        from e in _context.Estado

                                        where u.TipoUsuario.All(x => x.Id != 1)
                                        where u.IdEstado == e.Id
                                        where u.IdEstado != 1

                                        select new { u.Id, Nombre = u.Nombre + " " + u.Apellido1 + " " + u.Apellido2, u.Correo, Estado = e.Nombre, eID = e.Id }



                                        );
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



        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Registro()
        {
            ViewBag.IdProvincia = ListaProvincias();
            ViewBag.IdCanton = ListaCanton();
            ViewBag.IdDistrito = ListaDistritos();
            ViewBag.IdTipoPago = ListaPago();
            return View();
        }

        private SelectList ListaPago(int idTipo = 0)
        {
            IServiceTipoPago _service = new ServiceTipoPago();
            IEnumerable<TipoPago> lista = _service.GetTipoPago();
            return new SelectList(lista, "Id", "Nombre", idTipo);
        }

        public SelectList ListaProvincias(int idProvincia = 0)
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest
                .Create("https://ubicaciones.paginasweb.cr/provincias.json");
            myReq.ContentType = "json";
            Response.ContentType = "json";

            try { 
            var response = (HttpWebResponse)myReq.GetResponse();
            string text;

            JObject json = new JObject();
            List<Provincia> ts = new List<Provincia>();


            using (var sr = new StreamReader(response.GetResponseStream()))
            {

                text = sr.ReadToEnd();
                json = JObject.Parse(text);

            }
            foreach (var item in json)
            {
                string id = item.Key;
                string nombre = (string)item.Value;
                Provincia provincia = new Provincia(id, nombre);
                ts.Add(provincia);
            }
            IEnumerable<Provincia> provincias = ts;


            return new SelectList(provincias, "Nombre", "Nombre", idProvincia);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al obtener datos " + ex.Message;
                TempData["Redirect"] = "Home";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return new SelectList("Null", idProvincia);
            }
        }

        public SelectList ListaCanton(string idProvincia = "1", int idCanton = 0)
        {

            var url = "https://ubicaciones.paginasweb.cr/provincia/" + idProvincia + "/cantones.json";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest
                .Create(url);
            myReq.ContentType = "json";
            Response.ContentType = "json";

            try { 
            var response = (HttpWebResponse)myReq.GetResponse();
            string text;

            JObject json = new JObject();
            List<Canton> ts = new List<Canton>();


            using (var sr = new StreamReader(response.GetResponseStream()))
            {

                text = sr.ReadToEnd();
                json = JObject.Parse(text);

            }
            foreach (var item in json)
            {
                string id = item.Key;
                string nombre = (string)item.Value;
                Canton canton = new Canton(id, nombre);
                ts.Add(canton);
            }
            IEnumerable<Canton> cantones = ts;

            return new SelectList(cantones, "Nombre", "Nombre", idCanton);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al obtener datos " + ex.Message;
                TempData["Redirect"] = "Home";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return new SelectList("Null", idCanton);
            }
        }

        public string RefreshCanton(string idProvincia = "1")
        {

            var url = "https://ubicaciones.paginasweb.cr/provincia/" + idProvincia + "/cantones.json";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest
                .Create(url);
            myReq.ContentType = "json";
            Response.ContentType = "json";


            var response = (HttpWebResponse)myReq.GetResponse();
            string text;

            List<Canton> ts = new List<Canton>();


            using (var sr = new StreamReader(response.GetResponseStream()))
            {

                text = sr.ReadToEnd();

            }

            return text;
        }


        public SelectList ListaDistritos(string idProvincia = "1", string idCanton = "1", int idDistrito = 0)
        {

            var url = "https://ubicaciones.paginasweb.cr/provincia/" + idProvincia + "/canton/"+ idCanton + "/distritos.json";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest
                .Create(url);
            myReq.ContentType = "json";
            Response.ContentType = "json";

            try { 
            var response = (HttpWebResponse)myReq.GetResponse();
            string text;

            JObject json = new JObject();
            List<Distrito> ts = new List<Distrito>();


            using (var sr = new StreamReader(response.GetResponseStream()))
            {

                text = sr.ReadToEnd();
                json = JObject.Parse(text);

            }
            foreach (var item in json)
            {
                string id = item.Key;
                string nombre = (string)item.Value;
                Distrito distrito = new Distrito(id, nombre);
                ts.Add(distrito);
            }
            IEnumerable<Distrito> distritos = ts;


            return new SelectList(distritos, "Nombre", "Nombre", idDistrito);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al obtener datos " + ex.Message;
                TempData["Redirect"] = "Home";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return new SelectList("Null", idDistrito);

            }
        }

        public string RefreshDistritos(string idProvincia = "1", string idCanton = "1")
        {

            var url = "https://ubicaciones.paginasweb.cr/provincia/" + idProvincia + "/canton/" + idCanton + "/distritos.json";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest
                .Create(url);
            myReq.ContentType = "json";
            Response.ContentType = "json";


            var response = (HttpWebResponse)myReq.GetResponse();
            string text;



            using (var sr = new StreamReader(response.GetResponseStream()))
            {

                text = sr.ReadToEnd();

            }



            return text;
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CambiarEstado(int idEstado, int idUsuario)
        {


            IServiceUsuario _Service = new ServiceUsuario();
            Usuario oUsuario = _Service.GetUsuarioByID(idUsuario);

            if (oUsuario == null) {
                ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Error",
        "El usuario consultado no existe.", SweetAlertMessageType.error);

                return View("Usuario");


            }
            oUsuario.IdEstado = idEstado;
            string[] selectedTipos = { };
            int[] arrayDirecciones = { };
            int[] arrayCuentas = { };



            try
            {
                Usuario rUsuario = _Service.Save(oUsuario, selectedTipos, arrayDirecciones, arrayCuentas);
                return RedirectToAction("Index","Usuario");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [HttpPost]
        public ActionResult SaveRegistro(Registro registro, HttpPostedFileBase ImageFile)
        {


            int[] arrayDirecciones = { };
            int[] arrayCuentas = { };
            MemoryStream target = new MemoryStream();
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IServiceDireccion _ServiceDireccion = new ServiceDireccion();
            IServiceCuentaPago _ServiceCuenta = new ServiceCuentaPago();
            string[] selectedTipos = new string[] { };

            if (registro.Foto == null)
            {
                if (ImageFile != null)
                {
                    ImageFile.InputStream.CopyTo(target);
                    registro.Foto = target.ToArray();
                    ModelState.Remove("Foto");
                }
                else
                {
                    ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Error",
                        "Es necesario seleccionar una foto.", SweetAlertMessageType.error);
                    ViewBag.IdProvincia = ListaProvincias();
                    ViewBag.IdCanton = ListaCanton();
                    ViewBag.IdDistrito = ListaDistritos();
                    ViewBag.IdTipoPago = ListaPago();
                    return View("Registro");
                }
            }

            ModelState.Remove("Estado");
            if (ModelState.IsValid)
            {

                if(DateTime.Now.Year > int.Parse(registro.Anio))
                {
                    if(DateTime.Now.Year == int.Parse(registro.Anio))
                    {
                        if (DateTime.Now.Month > int.Parse(registro.Mes))
                        {
                            ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Error",
            "La tarjeta que ingreso esta vencida correspondiendo a la fecha de expiración", SweetAlertMessageType.error);
                            ViewBag.IdProvincia = ListaProvincias();
                            ViewBag.IdCanton = ListaCanton();
                            ViewBag.IdDistrito = ListaDistritos();
                            ViewBag.IdTipoPago = ListaPago();
                            return View("Registro");
                        }
                    }
                    else
                    {
                        ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Error",
"La tarjeta que ingreso esta vencida correspondiendo a la fecha de expiración", SweetAlertMessageType.error);
                        ViewBag.IdProvincia = ListaProvincias();
                        ViewBag.IdCanton = ListaCanton();
                        ViewBag.IdDistrito = ListaDistritos();
                        ViewBag.IdTipoPago = ListaPago();
                        return View("Registro");
                    }

                }

                Usuario _Usuario = new Usuario
            {
                Apellido1 = registro.Apellido1,
                Apellido2 = registro.Apellido2,
                Cedula = registro.Cedula,
                Clave = registro.Clave,
                Correo = registro.Correo,
                Genero = registro.Genero,
                IdEstado = 1,
                Nombre = registro.Nombre,
                Foto = registro.Foto,
                Telefono = registro.Telefono
            };
                Direccion direccion = new Direccion
            {
                Provincia = registro.Provincia,
                Canton = registro.Canton,
                Distrito = registro.Distrito,
                DireccionExacta = registro.Senas
            };

                DateTime fecha = DateTime.Parse(registro.Anio + "-" + registro.Mes + "-01");
                DateTime lastDate = new DateTime(fecha.Year, fecha.Month, DateTime.DaysInMonth(fecha.Year, fecha.Month));

                CuentaPago cuentaPago = new CuentaPago
                {
                    CodSeguridad = registro.CodSeguridad,
                    FechaExp = lastDate,
                    IdTipoPago = registro.IdTipoPago,
                    NumCuenta = registro.NumCuenta,
                    
                };

                Usuario vUsuario = _ServiceUsuario.GetUsuarioByEmail(_Usuario.Correo);
                if(vUsuario != null)
            {
                ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Error",
    "Ya existe una cuenta vinculada a este correo.", SweetAlertMessageType.error);
                ViewBag.IdProvincia = ListaProvincias();
                ViewBag.IdCanton = ListaCanton();
                ViewBag.IdDistrito = ListaDistritos();
                ViewBag.IdTipoPago = ListaPago();
                return View("Registro");
            } 

                if (registro.Cliente == true || registro.Proveedor == true)
                {

                    if (registro.Cliente != false)
                    {
                        selectedTipos = selectedTipos.Append("2").ToArray();
                    }
                    if (registro.Proveedor != false)
                    {
                        selectedTipos = selectedTipos.Append("3").ToArray();
                    }



                try
                {
                        CuentaPago oCuenPago = _ServiceCuenta.Save(cuentaPago);
                        arrayCuentas = arrayCuentas.Append(oCuenPago.Id).ToArray();
                        Direccion oDireccion = _ServiceDireccion.Save(direccion);
                        arrayDirecciones = arrayDirecciones.Append(oDireccion.Id).ToArray();

                        Usuario oUsuario = _ServiceUsuario.Save(_Usuario, selectedTipos, arrayDirecciones, arrayCuentas);

                    return RedirectToAction("Index", "Home");
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
                else {
                ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Error",
                    "Debes de seleccionar un tipo de cuenta, sea cliente o proveedor, o ambos.", SweetAlertMessageType.error);
                ViewBag.IdProvincia = ListaProvincias();
                ViewBag.IdCanton = ListaCanton();
                ViewBag.IdDistrito = ListaDistritos();
                ViewBag.IdTipoPago = ListaPago();
                return View("Registro");

            }
            }
            else
            {
                // Valida Errores si Javascript está deshabilitado
                Utils.Util.ValidateErrors(this);
                //Recurso a cargar en la vista

                //Debe funcionar para crear y modificar

                ViewBag.IdProvincia = ListaProvincias();
                ViewBag.IdCanton = ListaCanton();
                ViewBag.IdDistrito = ListaDistritos();
                ViewBag.IdTipoPago = ListaPago();
                return View("Registro");
            }
        }


    }
}
