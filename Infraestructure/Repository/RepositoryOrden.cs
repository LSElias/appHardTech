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
                    list = ctx.Orden.ToList<Orden>();
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

        public Orden GetOrdenById(int Id)
        {
            try
            {
                Orden pOrden = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    pOrden = ctx.Orden.
                        Include("OrdenDetalle")
                        .Include("Factura")
                        .Where(x => x.Id == Id)
                        .FirstOrDefault();
                }
                return pOrden;
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

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                pOrden = GetOrdenById((int)orden.Id);

                if (pOrden == null)
                {
                    //Insertar
                    ctx.Orden.Add(orden);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    //Actualizar
                    ctx.Orden.Add(orden);
                    ctx.Entry(orden).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }
            if (retorno >= 0)
                pOrden = GetOrdenById((int)orden.Id);

            return pOrden;
        }
    }
}
