using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                        .Where(x=>x.IdCategoria == IdCategoria)
                        .ToList();
                }
                return oProducto;
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
                        .Where(x => x.IdProveedor  == IdProveedor)
                        .ToList();
                }
                return oProducto;
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
            try
            {
                Producto oProducto = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = ctx.Producto.Find(Id);
                }
                return oProducto;
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
                    list = ctx.Producto.ToList<Producto>();
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

        public Producto Save(Producto producto)
        {
            int retorno = 0;
            Producto oProducto = null;

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
    }
}
