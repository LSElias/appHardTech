using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;
using Web.ViewModel;

namespace Web.Controllers
{
    public class BilleteraController : Controller
    {
        // GET: Billetera
        [CustomAuthorize((int)Roles.Cliente, (int)Roles.Proveedor)]
        public ActionResult MiBilletera()
        {
            return View();
        }

        public ActionResult LoadCuentas()
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
                    var productoData = (from d in _context.CuentaPago
                                        from u in _context.Usuario
                                        from t in _context.TipoPago

                                        where u.Id == id
                                        where d.Usuario1.Any(x => x.Id == u.Id)
                                        where d.IdTipoPago == t.Id

                                        select new { d.Id, t.Nombre, d.FechaExp});

                    // Organización
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        productoData = productoData.OrderBy(sortColumn + " " + sortColumnDir);
                    }
                    // Buscar

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
                Web.Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        public PartialViewResult Detalle(int id)
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

            IServiceCuentaPago _ServiceUsuario = new ServiceCuentaPago();

            CuentaPago oCuentaPago = new CuentaPago();
            oCuentaPago = _ServiceUsuario.GetCuentaPagoByID(id);

            return PartialView("_PartialDetC", oCuentaPago);
        }



        // GET: Billetera/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Billetera/Create
        [CustomAuthorize((int)Roles.Cliente, (int)Roles.Proveedor)]
        public ActionResult Crear()
        {

            ViewBag.IdTipoPago = ListaPago();

            return View();
        }

        private SelectList ListaPago(int idTipo = 0)
        {
            IServiceTipoPago _service = new ServiceTipoPago();
            IEnumerable<TipoPago> lista = _service.GetTipoPago();
            return new SelectList(lista, "Id", "Nombre", idTipo);
        }

        // GET: Billetera/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Billetera/Edit/5
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

        // GET: Billetera/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Billetera/Delete/5
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
        public ActionResult Save(Registro registro)
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

            if (DateTime.Now.Year > int.Parse(registro.Anio))
            {
                if (DateTime.Now.Year == int.Parse(registro.Anio))
                {
                    if (DateTime.Now.Month > int.Parse(registro.Mes))
                    {
                        ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Error",
        "La tarjeta que ingreso esta vencida correspondiendo a la fecha de expiración", SweetAlertMessageType.error);
                        ViewBag.IdTipoPago = ListaPago();
                        return View("Crear");
                    }
                }
                else
                {
                    ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Error",
"La tarjeta que ingreso esta vencida correspondiendo a la fecha de expiración", SweetAlertMessageType.error);
                    ViewBag.IdTipoPago = ListaPago();
                    return View("Crear");
                }

            }




            DateTime fecha = DateTime.Parse(registro.Anio + "-" + registro.Mes + "-01");
            DateTime lastDate = new DateTime(fecha.Year, fecha.Month, DateTime.DaysInMonth(fecha.Year, fecha.Month));

            IServiceCuentaPago _Service = new ServiceCuentaPago();
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario _Usuario = _ServiceUsuario.GetUsuarioByID(_idUsuario);
            CuentaPago _cuentaPago = new CuentaPago
            {
                CodSeguridad = registro.CodSeguridad,
                FechaExp = lastDate,
                IdTipoPago = registro.IdTipoPago,
                NumCuenta = registro.NumCuenta,

            };

            try
            {
                ModelState.Remove("Nombre");
                ModelState.Remove("Apellido1");
                ModelState.Remove("Apellido2");
                ModelState.Remove("Cedula");
                ModelState.Remove("Telefono");
                ModelState.Remove("Correo");
                ModelState.Remove("Clave");
                ModelState.Remove("ConfirmarClave");
                ModelState.Remove("Foto");
                ModelState.Remove("Genero");
                ModelState.Remove("Cliente");
                ModelState.Remove("Proveedor");
                ModelState.Remove("Provincia");
                ModelState.Remove("Canton");
                ModelState.Remove("Distrito");
                ModelState.Remove("Senas");

                if (ModelState.IsValid)
                {
                    if (_cuentaPago.Id == 0)
                    {
                        CuentaPago oBilletera = _Service.Save(_cuentaPago);
                        IEnumerable<CuentaPago> cuentasUser = _Service.GetCuentaByIdUsuario(_idUsuario);
                        int[] idCuenta = { };

                        foreach (CuentaPago cuentaPago in cuentasUser)
                        {
                            idCuenta = idCuenta.Append(cuentaPago.Id).ToArray();
                        }

                        idCuenta = idCuenta.Append(oBilletera.Id).ToArray();
                        string[] roles = { };
                        int[] direcciones = { };

                        Usuario sUsuario = _ServiceUsuario.Save(_Usuario, roles, direcciones, idCuenta);
                    }
                    else
                    {
                        CuentaPago oBilletera = _Service.Save(_cuentaPago);

                    }
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    //Recurso a cargar en la vista

                    //Debe funcionar para crear y modificar
                    ViewBag.IdTipoPago = ListaPago();
                    return View("Crear", registro);
                }
                TempData["Message"] = SweetAlertHelper.Mensaje("¡Hecho!",
"Dirección creada", SweetAlertMessageType.success);

                return RedirectToAction("MiBilletera");
            }
            catch (Exception ex)
            {
                Web.Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Billetera";
                TempData["Redirect-Action"] = "MiBilletera";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

    }
}
