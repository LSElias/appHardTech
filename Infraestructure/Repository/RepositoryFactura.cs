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
    public class RepositoryFactura : IRepositoryFactura
    {
        public IEnumerable<Factura> GetFactura()
        {
            try
            {
                IEnumerable<Factura> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Factura.ToList<Factura>();
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

        public Factura GetFacturaById(int IdFactura)
        {
            Factura oFactura = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oFactura = ctx.Factura
                        .Include("Usuario")
                        .Include("Orden")
                        .Where(x => x.IdFactura== IdFactura)
                        .FirstOrDefault();
                }
                return oFactura;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Factura GetFacturaByIdOrden(int IdOrden)
        {
            try
            {
                Factura oFactura = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oFactura = ctx.Factura
                        .Include("Usuario")
                        .Include("Orden")
                        .Where(x=> x.IdOrden == IdOrden)
                        .FirstOrDefault();
                }
                return oFactura;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Factura> GetFacturaByIdUsuario(int IdUsuario)
        {
            try
            {
                IEnumerable <Factura> oFactura = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oFactura = ctx.Factura
                        .Include("Usuario")
                        .Include("Orden")
                        .Where(x => x.IdUsuario == IdUsuario)
                        .ToList();
                }
                return oFactura;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Factura Save(Factura factura)
        {
            int retorno = 0;
            Factura pFactura = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                pFactura = GetFacturaById((int)factura.IdFactura);

                if (pFactura == null)
                {
                    //Insertar
                    ctx.Factura.Add(factura);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    //Actualizar
                    ctx.Factura.Add(factura);
                    ctx.Entry(factura).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }
            if (retorno >= 0)
                pFactura = GetFacturaById((int)factura.IdFactura);

            return pFactura;
        }
    }
}
