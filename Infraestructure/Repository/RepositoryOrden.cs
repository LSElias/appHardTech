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
    public class RepositoryOrden : IRepositoryOrden
    {
        public IEnumerable<Factura> GetOrden()
        {
            try
            {
                IEnumerable<Factura> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Orden.
                            Include("Estado").
                            ToList<Factura>();
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

        public Factura GetOrdenById(int Id)
        {
            Factura pOrden = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    pOrden = ctx.Orden.
                        Include("Estado").
                        Include("Factura").
                        Include("OrdenDetalle").
                        Include("OrdenDetalle.Producto")
                        .Where(x => x.IdOrden == Id)
                        .FirstOrDefault<Factura>();
                }
                return pOrden;
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

        public Factura Save(Factura orden)
        {
            int retorno = 0;
            Factura pOrden = null;

            try
            {
              using(MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    pOrden = GetOrdenById((int)orden.IdOrden);

                   if (pOrden == null)
                    {
                        //Guardar los datos en ambos tablas Orden y OrdenDet...
                        using (var transaccion = ctx.Database.BeginTransaction())
                        {
                            ctx.Orden.Add(orden);
                            retorno = ctx.SaveChanges();
                            transaccion.Commit();
                        }
                    }
                    else
                    {
                        ctx.Orden.Add(orden);
                        ctx.Entry(orden).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }

              if(retorno >= 0) 
                 pOrden = GetOrdenById(orden.IdOrden);


                return pOrden; 
                
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
