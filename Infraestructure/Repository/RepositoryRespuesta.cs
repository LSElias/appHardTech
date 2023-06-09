﻿using Infraestructure.Models;
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
    public class RepositoryRespuesta : IRepositoryRespuesta
    {
        public IEnumerable<Respuesta> GetRespuesta()
        {
            try
            {
                IEnumerable<Respuesta> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Respuesta.
                        Include("Mensaje").
                        Include("Usuario").
                        ToList<Respuesta>();
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
                throw new Exception(mensaje);
            }
        }

        public Respuesta GetRespuestaById(int Id)
        {
            try
            {
                Respuesta oRespuesta = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oRespuesta = ctx.Respuesta.
                        Include("Mensaje")
                        .Include("Usuario")
                        .Where(x => x.Id == Id)
                        .FirstOrDefault(); 
                }
                return oRespuesta;
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
                throw new Exception(mensaje);
            }
        }

        public Respuesta GetRespuestaByIdMensaje(int IdMensaje)
        {
            try
            {
                Respuesta oRespuesta = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oRespuesta = ctx.Respuesta
                        .Include("Mensaje")
                        .Include("Usuario")
                        .Where(x => x.IdMensaje == IdMensaje)
                        .FirstOrDefault();
                }
                return oRespuesta;
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
                throw new Exception(mensaje);
            }
        }

        public IEnumerable<Respuesta> GetRespuestaByIdProveedor(int IdProveedor)
        {
            try
            {
                IEnumerable<Respuesta> oRespuesta = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oRespuesta = ctx.Respuesta
                        .Include("Mensaje")
                        .Include("Usuario")
                        .Where(x => x.IdProveedor == IdProveedor)
                        .ToList();
                }
                return oRespuesta;
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
                throw new Exception(mensaje);
            }
        }

        public Respuesta Save(Respuesta respuesta)
        {
            int retorno = 0;
            Respuesta oRespuesta = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oRespuesta = GetRespuestaById((int)respuesta.Id);

                    if (oRespuesta == null)
                    {
                        //Insertar
                        ctx.Respuesta.Add(respuesta);
                        retorno = ctx.SaveChanges();
                    }
                    else
                    {
                        //Actualizar
                        ctx.Respuesta.Add(respuesta);
                        ctx.Entry(respuesta).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
                if (retorno >= 0)
                    oRespuesta = GetRespuestaById((int)respuesta.Id);

                return oRespuesta;
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
                throw new Exception(mensaje);
            }


        }
    }
}
