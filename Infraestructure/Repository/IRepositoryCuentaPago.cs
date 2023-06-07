﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryCuentaPago
    {
        IEnumerable<CuentaPago> GetCuentaPagos();
        CuentaPago GetCuentaPagoByID(int Id);
        IEnumerable<CuentaPago> GetByTipoPago(int IdTipoPago);
    }
}
