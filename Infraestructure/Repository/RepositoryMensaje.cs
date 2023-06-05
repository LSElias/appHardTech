using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryMensaje : IRepositoryMensaje
    {
        public IEnumerable<Mensaje> GetMensaje()
        {
            try
            {
                IEnumerable<Mensaje> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Mensaje.ToList<Mensaje>();
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

        public Mensaje GetMensajeById(int Id)
        {
            try
            {
                Mensaje oMensaje = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMensaje = ctx.Mensaje.Find(Id);
                }
                return oMensaje;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Mensaje GetMensajeByIdProducto(int idProducto)
        {
            try
            {
                Mensaje oMensaje = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMensaje = ctx.Mensaje.Find(idProducto);
                }
                return oMensaje;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Mensaje GetMensajeByIdUsuario(int IdUsuario)
        {
            try
            {
                Mensaje oMensaje = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMensaje = ctx.Mensaje.Find(IdUsuario);
                }
                return oMensaje;
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
