﻿using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryOrdenDetalle : IRepositoryOrdenDetalle
    {
        public IEnumerable<OrdenDetalle> GetOrdenDetalle()
        {
            try
            {
                IEnumerable<OrdenDetalle> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.OrdenDetalle.ToList<OrdenDetalle>();
                }
                return list;

            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public OrdenDetalle GetOrdenDetByIdOrden(int IdOrden)
        {
            try
            {
                OrdenDetalle oDetalle = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oDetalle = ctx.OrdenDetalle
                        .Include("Orden")
                        .Include("Producto")
                        .Where(x => x.IdOrden == IdOrden)
                        .FirstOrDefault();
                }
                return oDetalle;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public OrdenDetalle GetOrdenDetByIdProducto(int idProducto)
        {
            try
            {
                OrdenDetalle oDetalle = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oDetalle = ctx.OrdenDetalle
                        .Include("Orden")
                        .Include("Producto")
                        .Where(x => x.idProducto == idProducto)
                        .FirstOrDefault();
                }
                return oDetalle;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public OrdenDetalle Save(OrdenDetalle ODetalle)
        {
            int retorno = 0;
            OrdenDetalle pOrden = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                pOrden = GetOrdenDetByIdOrden((int)ODetalle.IdOrden);

                if (pOrden == null)
                {
                    //Insertar
                    ctx.OrdenDetalle.Add(ODetalle);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    //Actualizar
                    ctx.OrdenDetalle.Add(ODetalle);
                    ctx.Entry(ODetalle).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }
            if (retorno >= 0)
                pOrden = GetOrdenDetByIdOrden((int)ODetalle.IdOrden);

            return pOrden;
        }
    }
}
