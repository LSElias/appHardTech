using ApplicationCore.Utils;
using Infraestructure.Models;
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
        public Usuario GetUsuario(string oCorreo, string oClave)
        {
           IRepositoryUsuario oRep = new RepositoryUsuario();
            string crytpPasswd = Cryptography.EncrypthAES(oClave);
            return oRep.GetUsuario(oCorreo, crytpPasswd);
        }

        public Usuario GetUsuarioByID(int Id)
        {
            IRepositoryUsuario oRep = new RepositoryUsuario();
            Usuario oUsuario = oRep.GetUsuarioByID(Id);
            oUsuario.Clave = Cryptography.DecrypthAES(oUsuario.Clave);
            
            return oUsuario;
        }

        public Usuario Save(Usuario usuario, string[] selectedTipoUsuario, int[] arrayDirecciones)
        {
            IRepositoryUsuario oRep = new RepositoryUsuario();
            usuario.Clave = Cryptography.EncrypthAES(usuario.Clave);
            return oRep.Save(usuario, selectedTipoUsuario, arrayDirecciones);
        }

        //No se encriptó porque es el Admin que usará los usuarios
        public IEnumerable<Usuario> GetUsuarioByEstado(int IdEstado)
        {
            IRepositoryUsuario oRep = new RepositoryUsuario();
            return oRep.GetUsuarioByEstado(IdEstado);
        }

        public IEnumerable<Usuario> GetUsuarioByIDTipoUsuario(int IdTipoUsuario)
        {
            IRepositoryUsuario oRep = new RepositoryUsuario();
            return oRep.GetUsuarioByIDTipoUsuario(IdTipoUsuario);
        }
    }
}
