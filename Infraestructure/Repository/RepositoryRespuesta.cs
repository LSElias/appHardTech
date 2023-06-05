using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryRespuesta : IRepositoryRespuesta
    {
        public IEnumerable<Respuesta> GetRespuesta()
        {
            try
            {
                IEnumerable<Respuesta> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Respuesta.ToList<Respuesta>();
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

        public Respuesta GetRespuestaById(int Id)
        {
            try
            {
                Respuesta oRespuesta = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oRespuesta = ctx.Respuesta.Find(Id);
                }
                return oRespuesta;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Respuesta GetRespuestaByIdMensaje(int IdMensaje)
        {
            try
            {
                Respuesta oRespuesta = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oRespuesta = ctx.Respuesta.Find(IdMensaje);
                }
                return oRespuesta;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Respuesta GetRespuestaByIdUsuario(int IdUsuario)
        {
            try
            {
                Respuesta oRespuesta = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oRespuesta = ctx.Respuesta.Find(IdUsuario);
                }
                return oRespuesta;
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
