using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryEvaluacion
    {
        IEnumerable<Evaluacion> GetEvaluacion();
        IEnumerable<Evaluacion> GetEvaluacionByIdEvaluador(int IdEvaluador);
        IEnumerable<Evaluacion> GetEvaluacionByIdEvaluado(int IdEvaluado);
        Evaluacion GetEvaluacionById(int Id);

        double GetPromedioEvaluacion(int IdEvaluado);

        Evaluacion Save(Evaluacion evaluacion);
    }
}
