using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Web.ViewModel
{
    public class Registro
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El primer apellido es requerido")]
        [Display(Name = "Primer apellido")]
        public string Apellido1 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El segundo apellido es requerido")]
        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La cédula es requerida")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "El formato válido debe estar conformado por nueve dígitos")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se acepta números")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El número de teléfono no es válido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El número de teléfono es requerido")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El correo electrónico es requerido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Correo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es requerida")]
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe confirmar la contraseña")]
        [Compare("Clave")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmarClave { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es requerida")]
        [Display(Name = "Fotografía")]
        public byte[] Foto { get; set; }

        [Required(ErrorMessage = "El género es requerido")]
        [Display(Name = "Género")]
        public string Genero { get; set; }

        [Display(Name = "Cuenta de Cliente")]
        public bool Cliente { get; set; }

        [Display(Name = "Cuenta de Proveedor")]
        public bool Proveedor { get; set; }

        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }

        [Required(ErrorMessage = "La dirección exacta es requerida requerido")]
        [Display(Name = "Dirección Exacta")]
        public string Senas { get; set; }
    }
}