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
                    oEvaluacion = ctx.Evaluacion
                        .Include("Usuario")
                        .Where(x => x.Id == Id)
                        .FirstOrDefault();
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

        public IEnumerable<Evaluacion> GetEvaluacionByIdEvaluado(int IdEvaluado)
        {
            try
            {
                IEnumerable<Evaluacion> oEvaluacion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEvaluacion = ctx.Evaluacion.
                        Where(x => x.IdEvaluado == IdEvaluado)
                        .Include("Usuario")
                        .ToList();
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

        public IEnumerable<Evaluacion> GetEvaluacionByIdEvaluador(int IdEvaluador)
        {
            try
            {
                IEnumerable<Evaluacion> oEvaluacion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEvaluacion = ctx.Evaluacion.
                        Where(x => x.IdEvaluador == IdEvaluador)
                        .Include("Usuario")
                        .ToList();
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

        /*
         Saca el promedio de evaluaciones dadas a una persona. 
         Hay que probarlo ya que puede ser que el código de momento este erroneo,
         si este es el caso, se cambiara en el futuro.
         */
        public double GetPromedioEvaluacion(int IdEvaluado)
        {
            IEnumerable<Evaluacion> lista = this.GetEvaluacionByIdEvaluado(IdEvaluado);
            double suma= 0;
            foreach(Evaluacion e in lista)
            {
                suma+= (int) e.Escala.Valor;
            }
            double promedio = suma / lista.Count();

            return promedio;
        }
    }
}
