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
    public class RepositoryCuentaPago : IRepositoryCuentaPago
    {
        public IEnumerable<CuentaPago> GetByTipoPago(int IdTipoPago)
        {
            IEnumerable<CuentaPago> list = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.CuentaPago
                        .Where(t => t.TipoPago.Id == IdTipoPago)
                        .Include("TipoPago")
                        .ToList();
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

        public CuentaPago GetCuentaPagoByID(int Id)
        {
            try
            {
                CuentaPago oCuenta = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCuenta = ctx.CuentaPago.
                        Include("TipoPago")
                        .Where(t => t.Id == Id)
                        .FirstOrDefault();
                }
                return oCuenta;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<CuentaPago> GetCuentaPagos()
        {
            try
            {
                IEnumerable<CuentaPago> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.CuentaPago.ToList<CuentaPago>();
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
    }
}
