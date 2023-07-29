using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Web.Models
{

    public partial class Provincia
    {
        public string Id { get; set; }
        public string Nombre { get; set; }

        public Provincia(string _id, string _nombre)
        {
            Id = _id;
            Nombre = _nombre;
        }
    }

    public partial class Canton
    {
        public string Id { get; set; }
        public string Nombre { get; set; }

        public Canton(string _id, string _nombre)
        {
            Id = _id;
            Nombre = _nombre;
        }
    }

    public partial class Distrito
    {
        public string Id { get; set; }
        public string Nombre { get; set; }

        public Distrito(string _id, string _nombre)
        {
            Id = _id;
            Nombre = _nombre;
        }
    }

}