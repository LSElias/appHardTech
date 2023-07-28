using ApplicationCore.Services;
using Infraestructure.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;
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


            ViewBag.IdTipoUsuario = ListaTipoUsuario();
            ViewBag.IdProvincia = ListaProvincias();
            ViewBag.IdCanton = ListaCanton();
            ViewBag.IdDistrito = ListaDistritos();

            return View();
        }
        public MultiSelectList ListaTipoUsuario(ICollection<TipoUsuario> categorias = null)
        {
            IServiceTipoUsuario _Service = new ServiceTipoUsuario();
            IEnumerable<TipoUsuario> lista = _Service.GetTipoUsuario();
            //Seleccionar las categorías para modificar
            int[] TipoUsuarioSelect = null;
            if (categorias != null)
            {
                TipoUsuarioSelect = categorias.Where(c => c.Id != 1).Select(c => c.Id).ToArray();
            }

            return new MultiSelectList(lista, "IdTipoUsuario", "Nombre", TipoUsuarioSelect);
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


            return new SelectList(provincias, "Id", "Nombre", idProvincia);
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


            return new SelectList(cantones, "Id", "Nombre", idCanton);
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


            return new SelectList(distritos, "Id", "Nombre", idDistrito);
        }



        // POST: Usuario/Create
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

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
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
    }
}
