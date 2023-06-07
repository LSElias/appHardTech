﻿using Infraestructure.Models;
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
        Evaluacion GetEvaluacionByIdEvaluador(int IdEvaluador);
        Evaluacion GetEvaluacionByIdEvaluado(int IdEvaluado);
        Evaluacion GetPromedioEvaluacion(int IdEvaluado);
    }
}
