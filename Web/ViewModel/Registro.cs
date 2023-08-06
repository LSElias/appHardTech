using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Xml.Linq;
using Web.Utils;

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
        [StringLength(24, MinimumLength = 8, ErrorMessage = "La contraseña debe de estar entre 8 y 24 carácteres")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$", ErrorMessage = "La contraseña debe de tener mínimo una letra en minúscula, una en mayúscula, un número y un caracter especial.")]
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

        [Required(ErrorMessage = "La provincia es requerida")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "El cantón es requerido")]
        public string Canton { get; set; }

        [Required(ErrorMessage = "El distrito es requerido")]
        public string Distrito { get; set; }

        [Required(ErrorMessage = "La dirección exacta es requerida ")]
        [Display(Name = "Dirección Exacta")]
        public string Senas { get; set; }

        [Required(ErrorMessage = "El tipo de tarjeta es requerido.")]
        [Display(Name = "Tipo de Tarjeta")]
        public int IdTipoPago { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se acepta números")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El número de tarjeta es requerida")]
        [StringLength(16, MinimumLength = 14, ErrorMessage = "La cantidad de dígitos no es válida")]
        [CreditCard]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Número de Tarjeta")]
        public string NumCuenta { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Digite el mes en números. Ej: 01")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Digite el mes en números. Ej: 01")]
        [Required(ErrorMessage = "El mes es requerido")]
        [Display(Name = "Mes")]
        public string Mes { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Digite el año en números. Ej: 2012")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Digite el año en números. Ej: 2012")]
        [Required(ErrorMessage = "El año es requerido")]
        
        [Display(Name = "Año")]
        public string Anio { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El código de seguridad es requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El código de seguridad solo acepta números")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "La cantidad de dígitos no es válida")]
        [Display(Name = "Código de seguridad")]
        public string CodSeguridad { get; set; }

    }
}