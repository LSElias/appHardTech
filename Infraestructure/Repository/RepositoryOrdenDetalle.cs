using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryOrdenDetalle : IRepositoryOrdenDetalle
    {
        public IEnumerable<OrdenDetalle> GetOrdenDetalle()
        {
            try
            {
                IEnumerable<OrdenDetalle> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.OrdenDetalle.ToList<OrdenDetalle>();
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

        public OrdenDetalle GetOrdenDetByIdOrden(int IdOrden)
        {
            try
            {
                OrdenDetalle oDetalle = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oDetalle = ctx.OrdenDetalle.Find(IdOrden);
                }
                return oDetalle;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public OrdenDetalle GetOrdenDetByIdProducto(int idProducto)
        {
            try
            {
                OrdenDetalle oDetalle = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oDetalle = ctx.OrdenDetalle.Find(idProducto);
                }
                return oDetalle;
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
