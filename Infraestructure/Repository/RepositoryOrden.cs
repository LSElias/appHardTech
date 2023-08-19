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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;

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

        //Reporte Administrador 

        //Reporte de las compras por semana
        public void GetOrdenByDia(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            DateTime inicioFec = DateTime.Now.Date.AddDays(-7);
            DateTime finFec = DateTime.Now.Date;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var resultado = ctx.Orden
                        //Traer las ordenes de la última semana 
                        .Where(m => m.FechaInicio >= inicioFec && m.FechaInicio <= finFec)
                        .GroupBy(z => z.FechaInicio)
                        .Select(m => new
                        {
                            FechaInicio = m.Key,
                            Cant = m.Count()
                        }).ToList();
                    foreach (var item in resultado)
                    {
                        varEtiquetas += String.Format("{0:dd/MM/yyyy}", item.FechaInicio) + ",";
                        varValores += item.Cant + ",";
                    }


                    varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                    varValores = varValores.Substring(0, varValores.Length - 1);

                    etiquetas = varEtiquetas;
                    valores = varValores;
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
                throw new Exception(mensaje);
            }
        }

        //Top Productos
        public void GetProductosTop(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            DateTime inicioFec = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime finFec = inicioFec.AddMonths(1).AddDays(1);

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var resultado = ctx.OrdenDetalle
                        .Where(m => m.Orden.FechaInicio >= inicioFec && m.Orden.FechaInicio <= finFec)
                        .GroupBy(z => z.Producto)
                        .OrderByDescending(group => group.Sum(c => c.Cantidad))
                        .Take(5)
                        .Select(group => new
                        {
                            NombreProduct = group.Key.Nombre,
                            Total = group.Sum(c => c.Cantidad)
                        })
                        .ToList();
                    foreach (var item in resultado)
                    {
                        varEtiquetas += item.NombreProduct + ", ";
                        varValores += item.Total + ", ";
                    }


                    varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                    varValores = varValores.Substring(0, varValores.Length - 1);

                    etiquetas = varEtiquetas;
                    valores = varValores;
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
                throw new Exception(mensaje);
            }
        }

        //Top 5 Vendedores 
        public void GetVendedoresTop(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var resultado = ctx.Usuario
                        .Where(x => x.TipoUsuario.Any(c => c.Nombre == "Proveedor"))
                        .Select(b => new
                        {
                            NombreVe = b.Nombre + " "+ b.Apellido1,
                            EvaluacionVen = b.Evaluacion1
                                .Select(c => c.Escala.Valor)
                                .Sum() /  b.Evaluacion1.Count()
                        })
                        .OrderByDescending(e => e.EvaluacionVen)
                        .Take(5)
                        .ToList(); 

                    foreach (var item in resultado)
                    {
                        varEtiquetas += item.NombreVe +  ", ";
                        varValores += item.EvaluacionVen + ", ";
                    }


                    varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                    varValores = varValores.Substring(0, varValores.Length - 1);

                    etiquetas = varEtiquetas;
                    valores = varValores;
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
                throw new Exception(mensaje);
            }
        }

        //Top 3 Vendedores Deficientes
        public void GetVendDeficiente(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var resultado = ctx.Usuario
                        .Where(x => x.TipoUsuario.Any(c => c.Nombre == "Proveedor"))
                        .Select(b => new
                        {
                            NombreVe = b.Nombre + " " + b.Apellido1,
                            EvaluacionVen = b.Evaluacion1
                                .Select(c => c.Escala.Valor)
                                .Sum() / b.Evaluacion1.Count()
                        })
                        .OrderBy(e => e.EvaluacionVen)
                        .Take(3)
                        .ToList();

                    foreach (var item in resultado)
                    {
                        varEtiquetas += item.NombreVe + ", ";
                        varValores += item.EvaluacionVen + ", ";
                    }


                    varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                    varValores = varValores.Substring(0, varValores.Length - 1);

                    etiquetas = varEtiquetas;
                    valores = varValores;
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
                throw new Exception(mensaje);
            }
        }

        //Reporte Proveedor 

        //Producto más vendido 
        public void GetMasVendidos(out string etiquetas, out string valores, int IdUsuario)
        {
            String varEtiquetas = "";
            String varValores = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var resultado = ctx.Usuario
                        .Where(v => v.Id == IdUsuario)
                        .SelectMany(v => v.Producto)
                        .OrderByDescending(p => p.VentasR)
                        .FirstOrDefault(); 

                    if (resultado != null)
                    {
                        varEtiquetas = resultado.Nombre + ", ";
                        varValores = resultado.VentasR + ", ";
                    }
                    else
                    {
                        varEtiquetas = "No existen resultados";
                        varValores = "";
                    }

                    etiquetas = varEtiquetas;
                    valores = varValores;
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
                throw new Exception(mensaje);
            }
        }


    }
}
