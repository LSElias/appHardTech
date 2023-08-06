using ApplicationCore.Services;
using Infraestructure.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Utils;

namespace Web.Controllers
{
    public class DireccionController : Controller
    {
        // GET: Direccion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MisDirecciones()
        {
            return View();
        }

        public ActionResult LoadDirecciones()
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
                    var productoData = (from d in _context.Direccion
                                        from u in _context.Usuario

                                        where u.Id == id
                                        where d.Usuario1.Any(x => x.Id == u.Id)

                                        select new { d.Id, d.Provincia, d.Canton, d.Distrito, d.DireccionExacta});

                    // Organización
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        productoData = productoData.OrderBy(sortColumn + " " + sortColumnDir);
                    }
                    // Buscar
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        productoData = productoData.Where(m => m.Provincia.ToLower().Contains(searchValue.ToLower()));
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


        // GET: Direccion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Direccion/Create
        public ActionResult Crear()
        {
            ViewBag.IdProvincia = ListaProvincias();
            ViewBag.IdCanton = ListaCanton();
            ViewBag.IdDistrito = ListaDistritos();
            return View();
        }

        // GET: Direccion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Direccion/Edit/5
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

        // GET: Direccion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Direccion/Delete/5
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
        public ActionResult Save(Direccion direccion, IEnumerable<HttpPostedFileBase> images, List<Foto> lista)
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
            IServiceDireccion _Service = new ServiceDireccion();
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario _Usuario = _ServiceUsuario.GetUsuarioByID(_idUsuario);

            try
            {
                if (ModelState.IsValid)
                {
                    Direccion odireccionI = _Service.Save(direccion);
                    int[] idDireccion = { odireccionI.Id };
                    Usuario sUsuario = _ServiceUsuario.Save(_Usuario, null, idDireccion, null);
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
                    return View("Crear", direccion);
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





        public SelectList ListaProvincias(int idProvincia = 0)
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest
                .Create("https://ubicaciones.paginasweb.cr/provincias.json");
            myReq.ContentType = "json";
            Response.ContentType = "json";

            try
            {
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

            try
            {
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

            var url = "https://ubicaciones.paginasweb.cr/provincia/" + idProvincia + "/canton/" + idCanton + "/distritos.json";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest
                .Create(url);
            myReq.ContentType = "json";
            Response.ContentType = "json";

            try
            {
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


    }
}
