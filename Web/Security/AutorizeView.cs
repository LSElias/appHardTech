using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Security
{
    public class AutorizeView
    {
        public static bool IsUserInRole(string[] nombreRoles)
        {
            IEnumerable<Roles> allowedroles = nombreRoles.
                Select(a => (Roles)Enum.Parse(typeof(Roles), a));
            bool authorize = false;
            var oUsuario = (Usuario)HttpContext.Current.Session["User"];
            if (oUsuario != null)
            {
                foreach (var role in oUsuario.TipoUsuario) { 
                    foreach (var rol in allowedroles)
                    {
                        if ((int)rol == role.Id )
                        return true;
                    }
                }
            }
            return authorize;
        }
    }

}