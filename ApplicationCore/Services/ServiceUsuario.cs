﻿using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public IEnumerable<Usuario> GetUsuario()
        {
           IRepositoryUsuario oRep = new RepositoryUsuario();
            return oRep.GetUsuario();
        }

        public IEnumerable<Usuario> GetUsuarioByEstado(int IdEstado)
        {
            IRepositoryUsuario oRep = new RepositoryUsuario();
            return oRep.GetUsuarioByEstado(IdEstado);
        }

        public Usuario GetUsuarioByID(int Id)
        {
            IRepositoryUsuario oRep = new RepositoryUsuario();
            return oRep.GetUsuarioByID(Id);
        }

        public IEnumerable<Usuario> GetUsuarioByIDTipoUsuario(int IdTipoUsuario)
        {
            IRepositoryUsuario oRep = new RepositoryUsuario();
            return oRep.GetUsuarioByIDTipoUsuario(IdTipoUsuario);
        }

        public Usuario LogIn(string email, string clave)
        {
            IRepositoryUsuario oRep = new RepositoryUsuario();
            return oRep.LogIn(email, clave);
        }

        public Usuario Save(Usuario usuario, string[] selectedTipoUsuario)
        {
            IRepositoryUsuario oRep = new RepositoryUsuario();
            return oRep.Save(usuario, selectedTipoUsuario);
        }
    }
}
