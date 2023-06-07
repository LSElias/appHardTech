using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryTipoUsuario : IRepositoryTipoUsuario
    {
        public IEnumerable<TipoUsuario> GetTipoUsuario()
        {
            try
            {
                IEnumerable<TipoUsuario> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.TipoUsuario.ToList<TipoUsuario>();
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

        public TipoUsuario GetTipoUsuarioByID(int Id)
        {
            try
            {
                TipoUsuario oTipo = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oTipo = ctx.TipoUsuario
                        .Where(x => x.Id == Id)
                        .FirstOrDefault();
                }
                return oTipo;
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
