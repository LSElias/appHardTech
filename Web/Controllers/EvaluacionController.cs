using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class EvaluacionController : Controller
    {
        // GET: Evaluacion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MisEvaluaciones()
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

            IServiceEvaluacion _ServiceEvaluacion = new ServiceEvaluacion();
            ViewBag.Evaluado = _ServiceEvaluacion.GetEvaluacionByIdEvaluado(id); 
            return View();
        }


        public ActionResult LoadData()
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
                    var productoData = ( from e in _context.Evaluacion
                                         from u in _context.Usuario
                                         from v in _context.Usuario
                                         from es in _context.Escala

                                         where e.IdEvaluador == u.Id
                                         where u.Id == id
                                         where v.Id == e.IdEvaluado
                                         where es.Id == e.IdEscala

                                         select new { e.Id, Nombre = v.Nombre + " " + v.Apellido1 + " " + v.Apellido2, Valor = es.Valor + "★" });

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

        public PartialViewResult EvaluarProveedor(int id)
        {


            IServiceEvaluacion _Service = new ServiceEvaluacion();
            Evaluacion oEvaluacion = _Service.GetEvaluacionById(id);


            return PartialView("_DetalleEv", oEvaluacion);
        }



        // GET: Evaluacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Evaluacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evaluacion/Create
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

        // GET: Evaluacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Evaluacion/Edit/5
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

        // GET: Evaluacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Evaluacion/Delete/5
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
