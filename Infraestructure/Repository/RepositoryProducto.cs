using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Util;

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
                        .Include("Estado_Producto")     
                        .Include("Foto")
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
                        .Include("Estado_Producto")
                        .Include("Foto")
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

        public Producto GetProductoById(int IdProducto)
        {
            Producto oProducto = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = ctx.Producto.
                        Where(n => n.IdProducto == IdProducto)
                        .Include("Categoria")
                        .Include("Usuario")
                        .Include("Estado_Producto")
                        .Include("Foto")
                        .Include("Mensaje")
                        .Include("Mensaje.Usuario")
                        .Include("Mensaje.Respuesta")
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
                    .Include("Estado_Producto")
                    .Include("Foto")
                    .ToList();
                }
                list = list.OrderBy(x => x.Precio);
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

        public Producto Save(Producto producto, IEnumerable<HttpPostedFileBase> images)
        {
            int retorno = 0;
            Producto oProducto = null;
            IRepositoryFoto _RepositoryFoto = new RepositoryFoto();
            IRepositoryUsuario _RepositoryUsu = new RepositoryUsuario();
            IRepositoryEstadoProducto _RepositoryEst = new RepositoryEstadoProducto();
            IRepositoryCategoria _RepositoryCat = new RepositoryCategoria();

            var imageList = new List<Foto>();

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = GetProductoById((int)producto.IdProducto);

                    if (oProducto == null)
                    {
                        //Intentar Guardar la foto y el producto 
                        using (var transaccion = ctx.Database.BeginTransaction())
                        {
                            //Insertar}
                            producto.VentasR = 0;
                            ctx.Producto.Add(producto);
                            retorno = ctx.SaveChanges();
                            transaccion.Commit();

                        }
                    }
                    else
                    {
                        //Actualizar
                        producto.Categoria = _RepositoryCat.GetCategoriaByID((int)oProducto.IdCategoria);

                        producto.Estado_Producto = _RepositoryEst.GetEstadoByID((int)producto.IdEstado);

                        producto.Usuario = _RepositoryUsu.GetUsuarioByID((int)oProducto.IdProveedor);
                        foreach(Foto foto in producto.Foto)
                        {
                            ctx.Foto.Attach(foto);
                        }
                        ctx.Categoria.Attach(producto.Categoria);
                        ctx.Estado_Producto.Attach(producto.Estado_Producto);
                        ctx.Usuario.Attach(producto.Usuario);
                        ctx.Producto.Add(producto);
                        ctx.Entry(producto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
                if (retorno >= 0)
                    oProducto = GetProductoById((int)producto.IdProducto);

                if (images != null)
                    {

                        foreach (var image in images)
                        {
                        if(image != null) { 
                            using (var br = new BinaryReader(image.InputStream))
                            {
                                var data = br.ReadBytes(image.ContentLength);
                                var img = new Foto { IdProducto = oProducto.IdProducto };
                                img.Media = data;
                                imageList.Add(img);
                                }
                            }
                        }
                        foreach (var foto in imageList)
                        {
                                var retornoo = _RepositoryFoto.Save(foto);
                        }
                    }
                oProducto = GetProductoById((int)producto.IdProducto);

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
