using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryEvaluacion : IRepositoryEvaluacion
    {
        public IEnumerable<Evaluacion> GetEvaluacion()
        {
            try
            {
                IEnumerable<Evaluacion> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Evaluacion.ToList<Evaluacion>();
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

        public Evaluacion GetEvaluacionById(int Id)
        {
            try
            {
                Evaluacion oEvaluacion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEvaluacion = ctx.Evaluacion.Find(Id);
                }
                return oEvaluacion;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Evaluacion GetEvaluacionByIdEvaluado(int IdEvaluado)
        {
            try
            {
                Evaluacion oEvaluacion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEvaluacion = ctx.Evaluacion.Find(IdEvaluado);
                }
                return oEvaluacion;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Evaluacion GetEvaluacionByIdEvaluador(int IdEvaluador)
        {
            try
            {
                Evaluacion oEvaluacion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEvaluacion = ctx.Evaluacion.Find(IdEvaluador);
                }
                return oEvaluacion;
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
