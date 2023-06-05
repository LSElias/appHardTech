using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
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
                    oFoto = ctx.Foto.Find(Id);
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

        public Foto GetFotoByIdProducto(int IdProducto)
        {
            try
            {
                Foto oFoto = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oFoto = ctx.Foto.Find(IdProducto);
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
    }
}
