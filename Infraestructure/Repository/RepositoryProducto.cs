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
    public class RepositoryProducto : IRepositoryProducto
    {
        public IEnumerable<Producto> GetByIdCategoria(int IdCategoria)
        {
            try
            {
                IEnumerable<Producto> oProducto = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = ctx.Producto
                        .Include("Categoria")
                        .Include("Usuario")
                        .Include("Estado1")                        
                        .Where(x=>x.IdCategoria == IdCategoria)
                        .ToList();
                }
                return oProducto;
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

        public IEnumerable<Producto> GetByIdProveedor(int IdProveedor)
        {
            try
            {
                IEnumerable<Producto> oProducto = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = ctx.Producto
                        .Include("Categoria")
                        .Include("Usuario")
                        .Include("Estado1")
                        .Where(x => x.IdProveedor  == IdProveedor)
                        .ToList();
                }
                return oProducto;
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

        public IEnumerable<Producto> GetByNombre(string Nombre)
        {
            try
            {
                IEnumerable<Producto> oProducto = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = ctx.Producto.ToList().
                          FindAll(x => x.Nombre.ToLower().Contains(Nombre.ToLower()));
                        
                }
                return oProducto;
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

        public Producto GetProductoById(int Id)
        {
            Producto oProducto = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = ctx.Producto.
                        Where(n => n.Id == Id)
                        .Include("Categoria")
                        .Include("Usuario")
                        .Include("Estado1")
                        .Include("Foto")
                        .Include("Mensaje")
                        .FirstOrDefault();
                }
                return oProducto;
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

        public IEnumerable<Producto> GetProductos()
        {
            try
            {
                IEnumerable<Producto> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Producto
                    .Include("Categoria")
                    .Include("Usuario")
                    .Include("Estado1")
                    .ToList<Producto>();
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

        public Producto Save(Producto producto)
        {
            int retorno = 0;
            Producto oProducto = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = GetProductoById((int)producto.Id);

                    if (oProducto == null)
                    {
                        //Insertar
                        ctx.Producto.Add(producto);
                        retorno = ctx.SaveChanges();
                    }
                    else
                    {
                        //Actualizar
                        ctx.Producto.Add(producto);
                        ctx.Entry(producto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
                if (retorno >= 0)
                    oProducto = GetProductoById((int)producto.Id);

                return oProducto;

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
