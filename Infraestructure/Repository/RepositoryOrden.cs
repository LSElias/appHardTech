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
        public IEnumerable<Orden> GetOrden()
        {
            try
            {
                IEnumerable<Orden> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Orden.
                            Include("Estado").
                            ToList<Orden>();
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

        public Orden GetOrdenById(int Id)
        {
            Orden pOrden = null;

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
                        .FirstOrDefault<Orden>();
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

        public Orden Save(Orden orden)
        {
            int retorno = 0;
            Orden pOrden = null;

            try
            {
              using(MyContext ctx = new MyContext())
                {
                    //Guardar los datos en ambos tablas Orden y OrdenDet...
                    using (var transaccion = ctx.Database.BeginTransaction())
                    {
                        ctx.Orden.Add(orden);
                        retorno = ctx.SaveChanges();
                        transaccion.Commit();
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
