using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    /*Prueba */
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public Usuario GetUsuario(string oCorreo, string oClave)
        {
            Usuario oUsuario = null; 

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario
                        .Where(p => p.Correo.Equals(oCorreo) && p.Clave == oClave).
                        FirstOrDefault<Usuario>();
                }

                if(oUsuario != null)
                oUsuario = GetUsuarioByID(oUsuario.Id);
                return oUsuario;  
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Usuario> GetUsuarioByEstado(int IdEstado)
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Usuario
                        .Where(x => x.IdEstado == IdEstado)
                         .Include("TipoUsuario")
                        .Include("IdEstado")
                        .ToList();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        /*
         Obtiene el usuario por ID.
        Parametros: int ID.
         */
        public Usuario GetUsuarioByID(int Id)
        {
            try
            {
                Usuario oUsuario = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario
                        .Include("TipoUsuario")
                        .Include("Estado")
                        .Where(u => u.Id == Id)
                        .FirstOrDefault<Usuario>();
                }
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        /*
         Retorna una lista de usuarios según su tipo de usuario.
        Parámetros: IDTipoUsuario
         */
        public IEnumerable<Usuario> GetUsuarioByIDTipoUsuario(int IdTipoUsuario)
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Usuario
                        .Include(c => c.TipoUsuario)
                        .Where(x => x.TipoUsuario.Any(t => t.Id == IdTipoUsuario))
                        .ToList();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario GetUsuarioByEmail(string correo)
        {
            try
            {
                Usuario oUsuario = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario
                        .Where(u => u.Correo == correo)
                        .FirstOrDefault<Usuario>();
                }
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario Save(Usuario usuario, string[] selectedTipoUsuario, int[] arrayDirecciones, int[] arrayCuentas)
        {
            int retorno = 0;
            Usuario oUsuario = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = GetUsuarioByID((int)usuario.Id);
                    IRepositoryTipoUsuario _RepositoryTipoUsuario = new RepositoryTipoUsuario();
                    IRepositoryDireccion _RepositoryDireccion = new RepositoryDireccion();
                    IRepositoryCuentaPago _RepositoryCuentaPago = new RepositoryCuentaPago();
                    IRepositoryEstado _RepositoryEstado = new RepositoryEstado();
                    
                    if (oUsuario == null)
                    {
                        if (selectedTipoUsuario != null)
                        {
                            usuario.TipoUsuario = new List<TipoUsuario>();
                            foreach (var tipoUser in selectedTipoUsuario)
                            {
                                var tipoUserAdd = _RepositoryTipoUsuario.GetTipoUsuarioByID(int.Parse(tipoUser));
                                ctx.TipoUsuario.Attach(tipoUserAdd);
                                usuario.TipoUsuario.Add(tipoUserAdd);
                            }
                        }
                        if (arrayDirecciones != null)
                        {
                            usuario.Direccion1 = new List<Direccion>();
                            foreach (var dirId in arrayDirecciones)
                            {
                                var dirAdd = _RepositoryDireccion.GetDireccionByID(dirId);
                                ctx.Direccion.Attach(dirAdd);
                                usuario.Direccion1.Add(dirAdd);
                            }
                        }

                        if (arrayCuentas != null)
                        {
                            usuario.CuentaPago1 = new List<CuentaPago>();
                            foreach (var cId in arrayCuentas)
                            {
                                var dirAdd = _RepositoryCuentaPago.GetCuentaPagoByID(cId);
                                ctx.CuentaPago.Attach(dirAdd);
                                usuario.CuentaPago1.Add(dirAdd);
                            }
                        }
                        //Insertar
                        ctx.Usuario.Add(usuario);
                        retorno = ctx.SaveChanges();
                    }
                    else
                    {
                        //Actualizar
                        Estado estado = _RepositoryEstado.GetEstadoByID((int) usuario.IdEstado);
                        ctx.Estado.Attach(estado);
                        usuario.Estado = estado;
                        foreach (TipoUsuario type in usuario.TipoUsuario)
                        {
                            string id = type.Id.ToString();
                            selectedTipoUsuario = selectedTipoUsuario.Append(id).ToArray();
                        }
                        usuario.TipoUsuario = null;
                        ctx.Usuario.Add(usuario);
                        ctx.Entry(usuario).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();

                        var selectedTipoUserID = new HashSet<string>(selectedTipoUsuario);
                        if (selectedTipoUsuario != null)
                        {

                            ctx.Entry(usuario).Collection(p => p.TipoUsuario).Load();

                            var newTipoUser = ctx.TipoUsuario
                             .Where(x => selectedTipoUserID.Contains(x.Id.ToString())).ToList();

                            usuario.TipoUsuario = newTipoUser;

                            ctx.Entry(usuario).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }



                        var DireccionesID = new HashSet<int>(arrayDirecciones);
                        if (DireccionesID != null && arrayDirecciones.Count() != 0)
                        {
                            ctx.Entry(usuario).Collection(x => x.Direccion1).Load();
                            var newDirForUser = ctx.Direccion
                                .Where(x => DireccionesID.Contains(x.Id)).ToList();
                            usuario.Direccion1 = newDirForUser;

                            ctx.Entry(usuario).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }


                        var CuentasID = new HashSet<int>(arrayCuentas);
                        if (CuentasID != null && arrayCuentas.Count() != 0)
                        {
                            ctx.Entry(usuario).Collection(x => x.CuentaPago1).Load();
                            var newCuentaForUser = ctx.CuentaPago
                                .Where(x => DireccionesID.Contains(x.Id)).ToList();
                            usuario.CuentaPago1 = newCuentaForUser;

                            ctx.Entry(usuario).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }

                    }
                }
                if (retorno >= 0)
                    oUsuario = GetUsuarioByID((int)usuario.Id);

                return oUsuario;

            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }

        }
    }
}
