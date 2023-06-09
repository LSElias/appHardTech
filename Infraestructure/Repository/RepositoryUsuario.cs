﻿using Infraestructure.Models;
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
    /*Prueba */
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
                    list = ctx.Usuario
                        .Include("TipoUsuario")
                        .Include("IdEstado")
                        .ToList();
                }
                return list;

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

        public IEnumerable<Usuario> GetUsuarioByEstado(int IdEstado)
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Usuario
                        .Where(x => x.IdEstado == IdEstado)
                         .Include("TipoUsuario")
                        .Include("IdEstado")
                        .ToList();
                }
                return lista;
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
                        .Include("IdEstado")
                        .Where(u => u.Id == Id)
                        .FirstOrDefault<Usuario>();
                }
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

        public Usuario Save(Usuario usuario, string[] selectedTipoUsuario)
        {
            int retorno = 0;
            Usuario oUsuario = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = GetUsuarioByID((int)usuario.Id);
                    IRepositoryTipoUsuario _RepositoryTipoUsuario = new RepositoryTipoUsuario();

                    if (oUsuario == null)
                    {
                        if (selectedTipoUsuario != null)
                        {
                            usuario.TipoUsuario = new List<TipoUsuario>();
                            foreach (var tipoUser in selectedTipoUsuario)
                            {
                                var tipoUserAdd = _RepositoryTipoUsuario.GetTipoUsuarioByID(int.Parse(tipoUser));
                                ctx.TipoUsuario.Add(tipoUserAdd);
                                usuario.TipoUsuario.Add(tipoUserAdd);
                            }
                        }

                        //Insertar
                        ctx.Usuario.Add(usuario);
                        retorno = ctx.SaveChanges();
                    }
                    else
                    {
                        //Actualizar
                        ctx.Usuario.Add(usuario);
                        ctx.Entry(usuario).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();

                        var selectedTipoUserID = new HashSet<string>(selectedTipoUsuario);
                        if (selectedTipoUsuario != null)
                        {
                            ctx.Entry(usuario).Collection(x => x.TipoUsuario).Load();
                            var newTipoUsForUser = ctx.TipoUsuario
                                .Where(x => selectedTipoUserID.Contains(x.Id.ToString())).ToList();
                            usuario.TipoUsuario = newTipoUsForUser;

                            ctx.Entry(usuario).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }
                    }
                }
                if (retorno >= 0)
                    oUsuario = GetUsuarioByID((int)usuario.Id);

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
