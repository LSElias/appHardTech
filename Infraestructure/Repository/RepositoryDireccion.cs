﻿using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryDireccion : IRepositoryDireccion
    {
        public Direccion GetDireccionByID(int Id)
        {
            Direccion oDireccion = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oDireccion = ctx.Direccion
                        .Include("Usuario")
                        .Where(x=>x.Id == Id)
                        .FirstOrDefault();
                }
                return oDireccion;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Direccion GetDireccionByIdUsuario(int IdUsuario)
        {
            try
            {
                Direccion oDireccion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oDireccion = ctx.Direccion
                        .Where(x => x.IdUsuario == IdUsuario)
                        .Include("Usuario")
                        .FirstOrDefault();

                }
                return oDireccion;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Direccion> GetDirecciones()
        {
            try
            {
                IEnumerable<Direccion> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Direccion.ToList<Direccion>();
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

        public Direccion Save(Direccion direccion)
        {
            int retorno = 0;
            Direccion pDireccion = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                pDireccion = GetDireccionByID((int)direccion.Id);

                if (pDireccion == null)
                {
                    //Insertar
                    ctx.Direccion.Add(direccion);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    //Actualizar
                    ctx.Direccion.Add(direccion);
                    ctx.Entry(direccion).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }
            if (retorno >= 0)
                pDireccion = GetDireccionByID((int)direccion.Id);

            return pDireccion;
        }
    }
}
