using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryMensaje : IRepositoryMensaje
    {
        public IEnumerable<Mensaje> GetMensaje()
        {
            try
            {
                IEnumerable<Mensaje> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Mensaje.
                        Include("Producto").
                        Include("Usuario").
                        ToList<Mensaje>();
                }
                return list;

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

        public Mensaje GetMensajeById(int Id)
        {
            try
            {
                Mensaje oMensaje = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMensaje = ctx.Mensaje.
                        Include("Producto").
                        Include("Usuario")                        .Where(x => x.Id == Id)
                        .FirstOrDefault();
                }
                return oMensaje;
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

        public IEnumerable<Mensaje> GetMensajeByIdProducto(int idProducto)
        {
            try
            {
                IEnumerable<Mensaje> oMensaje = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMensaje = ctx.Mensaje
                        .Where(x => x.idProducto == idProducto)
                        .Include("Producto").
                         Include("Usuario")
                        .ToList();

                    return oMensaje;
                }
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

        public IEnumerable<Mensaje> GetMensajeByIdUsuario(int IdUsuario)
        {
            try
            {
                IEnumerable<Mensaje> oMensaje = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMensaje = ctx.Mensaje
                        .Where(x => x.IdUsuario == IdUsuario)
                        .Include("Producto")
                        .Include("Usuario")
                        .ToList();
                }
                return oMensaje;
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

        public Mensaje Save(Mensaje mensaje)
        {
            int retorno = 0;
            Mensaje pMensaje = null;

           try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    pMensaje = GetMensajeById((int)mensaje.Id);

                    if (pMensaje == null)
                    {
                        //Insertar
                        ctx.Mensaje.Add(mensaje);
                        retorno = ctx.SaveChanges();
                    }
                    else
                    {
                        //Actualizar
                        ctx.Mensaje.Add(mensaje);
                        ctx.Entry(mensaje).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
                if (retorno >= 0)
                    pMensaje = GetMensajeById((int)mensaje.Id);

                return pMensaje;
            }

            catch (DbUpdateException dbEx)
            {
                string mensajeEx = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensajeEx);
                throw new Exception(mensajeEx);
            }
            catch (Exception ex)
            {
                string mensajeEx = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensajeEx);
                throw;
            }


        }
    }
}
