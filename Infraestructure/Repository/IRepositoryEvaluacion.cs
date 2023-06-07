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
        Evaluacion GetEvaluacionById(int Id);
        IEnumerable<Evaluacion> GetEvaluacionByIdEvaluador(int IdEvaluador);
        IEnumerable<Evaluacion> GetEvaluacionByIdEvaluado(int IdEvaluado);
        double GetPromedioEvaluacion(int IdEvaluado);
    }
}
