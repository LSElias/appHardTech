using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    //evitar cambio
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
                            Include("Estado_Orden").
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
                        Include("Estado_Orden").
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
            
            
            IRepositoryEstadoProducto _rEstProducto = new RepositoryEstadoProducto();
            IRepositoryEstadoOrden _rEstOrden = new RepositoryEstadoOrden();
            IRepositoryEstadoDetalle _rEstDetalle = new RepositoryEstadoDetalle();



            IRepositoryUsuario _rUsuario = new RepositoryUsuario();
            IRepositoryProducto _rProducto = new RepositoryProducto();
            IRepositoryUsuario _RepositoryUsu = new RepositoryUsuario();
            IRepositoryCategoria _RepositoryCat = new RepositoryCategoria();
            IRepositoryFactura _RepositoryFact = new RepositoryFactura();



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
                            foreach (OrdenDetalle detalle in orden.OrdenDetalle)
                            {
                                Producto oProducto = _rProducto.GetProductoById(detalle.IdProducto);
                                oProducto.Categoria = _RepositoryCat.GetCategoriaByID((int)oProducto.IdCategoria);
                                oProducto.Estado_Producto = _rEstProducto.GetEstadoByID((int)oProducto.IdEstado);
                                ctx.Producto.Attach(oProducto);

                            }


                            ctx.Estado_Orden.Attach(_rEstOrden.GetEstadoByID((int)orden.IdEstado));



                            ctx.Orden.Add(orden);
                            retorno = ctx.SaveChanges();
                            transaccion.Commit();
                        }
                    }
                    else
                    {
                        foreach (Factura facturaton in orden.Factura)
                        {
                            Factura oFactura = _RepositoryFact.GetFacturaSimpleByIdOrden((int)facturaton.IdOrden);
                            ctx.Factura.Attach(facturaton);
                            ctx.Entry(facturaton).State = EntityState.Modified;
                        }



                        foreach (OrdenDetalle detalle in orden.OrdenDetalle)
                        {

                            ctx.OrdenDetalle.Attach(detalle);
                            ctx.Entry(detalle).State = EntityState.Modified;
                        }


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

        //Reporte
        public void GetOrdenByDia(out string valores, out string etiquetas)
        {
            string varCantidad = "";
            DateTime varfechaHoy = DateTime.Today;
            DateTime varfechaTomorr = varfechaHoy.AddDays(1);
            string varHora = ""; 

            try
            {
                using(MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var resultado = ctx.Orden
                            .Where(c => c.FechaInicio >= varfechaHoy && c.FechaInicio < varfechaTomorr)
                            .GroupBy(c => c.FechaInicio.Value.Hour)
                            .Select(group => new
                            {
                                Hora = group.Key,
                                Cantidad = group.Count()
                            })
                            .OrderBy(entry => entry.Hora)
                            .ToList();
                  
                   foreach(var item in resultado) 
                   {
                        varCantidad += item.Cantidad;
                        varHora += item.Hora.ToString("HH:mm"); 
                   }
                }
                if(!string.IsNullOrEmpty(varCantidad))
                {
                    varCantidad = varCantidad.Substring(0, varCantidad.Length - 1);
                }
                if (!string.IsNullOrEmpty(varHora))
                {
                    varHora = varHora.Substring(0, varHora.Length - 1);
                }
                 valores = varCantidad;
                 etiquetas = varHora; 
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
