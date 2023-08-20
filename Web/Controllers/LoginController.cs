using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = null;
            try
            {
                ModelState.Remove("Nombre");
                ModelState.Remove("Apellido1");
                ModelState.Remove("Apellido2");
                ModelState.Remove("Cedula");
                ModelState.Remove("Telefono");
                ModelState.Remove("Genero");
                ModelState.Remove("TipoUsuario");
                ModelState.Remove("IdEstado");
                ModelState.Remove("Estado");

                if (ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.GetUsuario(usuario.Correo, usuario.Clave);
                    if (oUsuario != null)
                    {
                        if(oUsuario.IdEstado  == 1)
                        {
                            Session["User"] = oUsuario;
                            Log.Info($"Accede{oUsuario.Nombre} {oUsuario.Apellido1} + {oUsuario.Apellido2} " +
                                $"con el rol {oUsuario.TipoUsuario.ToList()}");
                            TempData["mensaje"] = Utils.SweetAlertHelper.Mensaje("Login",
                                "Usuario autenticado", Utils.SweetAlertMessageType.success);

                            if(oUsuario.TipoUsuario.Any(c => c.Nombre == "Administrador"))
                            {
                                return RedirectToAction("Administrador", "Dashboard");
                            }

                            if (oUsuario.TipoUsuario.Any(c => c.Nombre == "Proveedor"))
                            {
                                return RedirectToAction("Proveedor", "Dashboard");

                            }

                            if (oUsuario.TipoUsuario.Any(c => c.Nombre == "Cliente"))
                            {
                                return RedirectToAction("Index", "Home");

                            }
                        }
                        else
                        {
                            Log.Warn($"Intento de inicio de secion{usuario.Correo}");
                            ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("No se logro iniciar sesión",
                                "Este usuario no esta activado. Contacte a un administrador.", Utils.SweetAlertMessageType.error);

                        }


                    }
                    else
                    {
                        Log.Warn($"Intento de inicio de secion{usuario.Correo}");
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Login",
                            "Usuario no válido", Utils.SweetAlertMessageType.warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            return View("Index");
        }
        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "No autorizado";
            if (Session["User"] != null)
            {
                Usuario usuario = Session["User"] as Usuario;
                Log.Warn($"No autorizado {usuario.Correo}");
            }
            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                Session.Remove("User");
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}
