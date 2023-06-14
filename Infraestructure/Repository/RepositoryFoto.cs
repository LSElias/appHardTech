using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infraestructure.Repository
{
    public class RepositoryFoto : IRepositoryFoto
    {
        public IEnumerable<Foto> GetFoto()
        {
            try
            {
                IEnumerable<Foto> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Foto.ToList<Foto>();
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

        public Foto GetFotoById(int Id)
        {
            try
            {
                Foto oFoto = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oFoto = ctx.Foto
                        .Include("Producto")
                        .Where(x => x.Id == Id)
                        .FirstOrDefault();
                }
                return oFoto;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Foto> GetFotoByIdProducto(int IdProducto)
        {
            try
            {
                IEnumerable<Foto> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Foto
                        .Where(x => x.IdProducto == IdProducto)
                        .Include("Producto")
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

        public Foto Save(Foto foto)
        {
            int retorno = 0;
            Foto pFoto = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                pFoto = GetFotoById((int)foto.Id);

                if (pFoto == null)
                {
                    //Insertar
                    ctx.Foto.Add(foto);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    //Actualizar
                    ctx.Foto.Add(foto);
                    ctx.Entry(foto).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }
            if (retorno >= 0)
                pFoto = GetFotoById((int)foto.Id);

            return pFoto;
        }
    }
}
