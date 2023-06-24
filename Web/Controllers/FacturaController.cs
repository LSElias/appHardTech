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

namespace Web.Controllers
{
    public class FacturaController : Controller
    {
        // GET: Factura
        public ActionResult IndexAdmin()
        {
            return View();
        }

        public ActionResult MisOrdenes()
        {
            return View();
        }

        public ActionResult LoadData(int id)
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
                    var productoData = (from f in _context.Factura
                                        from o in _context.Orden
                                        from u in _context.Usuario
                                        from e in _context.Estado

                                        where f.IdUsuario == id
                                        where f.IdOrden == o.Id
                                        where o.IdEstado == e.Id
                                        where f.IdUsuario == u.Id
                                        select new { Id = f.IdFactura,IdOrden = o.Id, u.Nombre, f.Total, f.Fecha, Estado = e.Nombre }); ;
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


        // GET: Factura/Details/5
        public ActionResult Details(int? id)
        {

                return View();

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

        // GET: Factura/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Factura/Edit/5
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

        // GET: Factura/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Factura/Delete/5
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
