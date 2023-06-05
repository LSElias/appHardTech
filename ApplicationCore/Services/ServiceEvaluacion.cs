using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEvaluacion : IServiceEvaluacion
    {
        public IEnumerable<Evaluacion> GetEvaluacion()
        {
           IRepositoryEvaluacion oRep = new RepositoryEvaluacion();
            return oRep.GetEvaluacion();
        }

        public Evaluacion GetEvaluacionById(int Id)
        {
            IRepositoryEvaluacion oRep = new RepositoryEvaluacion();
            return oRep.GetEvaluacionById(Id);
        }

        public Evaluacion GetEvaluacionByIdEvaluado(int IdEvaluado)
        {
            IRepositoryEvaluacion oRep = new RepositoryEvaluacion();
            return oRep.GetEvaluacionByIdEvaluado(IdEvaluado);
        }

        public Evaluacion GetEvaluacionByIdEvaluador(int IdEvaluador)
        {
            IRepositoryEvaluacion oRep = new RepositoryEvaluacion();
            return oRep.GetEvaluacionByIdEvaluador(IdEvaluador);
        }
    }
}
