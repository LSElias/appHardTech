﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceCuentaPago
    {
        IEnumerable<CuentaPago> GetCuentaPagos();
        IEnumerable<CuentaPago> GetByTipoPago(int IdTipoPago);
        IEnumerable<CuentaPago> GetCuentaByIdUsuario(int IdUsuario);

        CuentaPago GetCuentaPagoByID(int Id);
        CuentaPago Save(CuentaPago cuenta);

    }
}
