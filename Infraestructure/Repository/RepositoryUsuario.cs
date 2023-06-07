using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public IEnumerable<Usuario> GetUsuario()
        {
            try
            {
                IEnumerable<Usuario> list = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Usuario.Include("TipoUsuario").ToList();
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

        public IEnumerable<Usuario> GetUsuarioByEstado(int Estado)
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Usuario
                        .Where(x => x.Estado == Estado)
                        .Include("TipoUsuario")
                        .ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        /*
         Obtiene el usuario por ID.
        Parametros: int ID.
         */
        public Usuario GetUsuarioByID(int Id)
        {
            try
            {
                Usuario oUsuario = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario
                        .Include("TipoUsuario")
                        .Where(u => u.Id == Id)
                        .FirstOrDefault<Usuario>();
                }
                return oUsuario;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
        /*
         Retorna una lista de usuarios según su tipo de usuario.
        Parámetros: IDTipoUsuario
         */
        public IEnumerable<Usuario> GetUsuarioByIDTipoUsuario(int IdTipoUsuario)
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Usuario
                        .Include(c => c.TipoUsuario)
                        .Where(x => x.TipoUsuario.Any(t => t.Id == IdTipoUsuario))
                        .ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario LogIn(string email, string clave)
        {
            Usuario oUsuario = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario.
                     Where(p => p.Correo.Equals(email) && p.Clave == clave).
                    FirstOrDefault<Usuario>();
                }
                if (oUsuario != null)
                    oUsuario = GetUsuarioByID(oUsuario.Id);
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
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
