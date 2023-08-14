﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
//evitar cambio
    public interface IRepositoryOrden
    {
        IEnumerable<Orden> GetOrden();
        Orden GetOrdenById(int Id);
        Orden Save(Orden orden);

        //Reporte
        void GetOrdenByDia(out string valores, out string etiquetas);
    }
}
