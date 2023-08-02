using Antlr.Runtime.Tree;
using ApplicationCore.Services;
using Infraestructure.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Web.Models;
using Web.Utils;
using static System.Net.WebRequestMethods;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
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

            return View();
        }

        public SelectList ListaProvincias(int idProvincia = 0)
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest
                .Create("https://ubicaciones.paginasweb.cr/provincias.json");
            myReq.ContentType = "json";
            Response.ContentType = "json"; 


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

        public SelectList ListaCanton(string idProvincia = "1", int idCanton = 0)
        {

            var url = "https://ubicaciones.paginasweb.cr/provincia/" + idProvincia + "/cantones.json";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest
                .Create(url);
            myReq.ContentType = "json";
            Response.ContentType = "json";


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
        public ActionResult Save(Usuario usuario, string IdProvincia, string IdCanton, string IdDistrito, HttpPostedFileBase ImageFile, string cliente, string proveedor, string senas)
        {
            int[] arrayDirecciones = { };
            MemoryStream target = new MemoryStream();
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IServiceDireccion _ServiceDireccion = new ServiceDireccion();
            string[] selectedTipos = new string[] { };
            if (proveedor != null)
            {
                selectedTipos = selectedTipos.Append("2").ToArray();
            }
            if (cliente != null)
            {
                selectedTipos = selectedTipos.Append("3").ToArray();
            }
            try
            {
                if (usuario.Foto == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        usuario.Foto = target.ToArray();
                        ModelState.Remove("Foto");
                    }

                }
                if(usuario.IdEstado == null)
                {
                    usuario.IdEstado = 1;
                }
                Direccion direccion = new Direccion();
                direccion.Provincia = IdProvincia;
                direccion.Canton = IdCanton;
                direccion.Distrito = IdDistrito;
                direccion.DireccionExacta = senas;
                Direccion oDireccion = _ServiceDireccion.Save(direccion);
                arrayDirecciones = arrayDirecciones.Append(oDireccion.Id).ToArray();


                ModelState.Remove("Estado");
                if (ModelState.IsValid)
                {
                    Usuario oUsuario = _ServiceUsuario.Save(usuario, selectedTipos, arrayDirecciones);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    //Recurso a cargar en la vista

                    //Debe funcionar para crear y modificar
                    return View("Registro", "Usuario");
                }

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


    }
}
