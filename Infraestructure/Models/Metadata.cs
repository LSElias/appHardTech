using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class CategoriaMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Categoría")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }

    }

    internal partial class CuentaPagoMetadata
    {
        public int Id { get; set; }
        public Nullable<int> IdTipoPago { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El número de tarjeta es requerida")]
        [StringLength(18, MinimumLength = 13, ErrorMessage = "La cantidad de dígitos no es válida")]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Número de cuenta")]
        public string NumCuenta { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es requerida")]
        [DataType(DataType.Date, ErrorMessage = "Fecha inválida")]
        [Display(Name = "Fecha de vencimiento")]
        public string FechaExp { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El código de seguridad es requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El código de seguridad solo acepta números")]
        [Display(Name = "Código de seguridad")]
        public string CodSeguridad { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        [Required(ErrorMessage = "El tipo de pago es requerido")]
        [Display(Name = "Tipo de pago")]
        public virtual TipoPago TipoPago { get; set; }

        [Display(Name = "Usuario")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Factura")]
        public virtual ICollection<Factura> Factura { get; set; }
    }

    internal partial class DireccionMetadata
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La provincia es requerida")]
        [Display(Name = "Provincia")]
        public string Provincia { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El cantón es requerido")]
        [Display(Name = "Cantón")]
        public string Canton { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El distrito es requerido")]
        [Display(Name = "Distrito")]
        public string Distrito { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La dirección exacta es requerida")]
        [Display(Name = "Dirección exacta")]
        public string DireccionExacta { get; set; }

    }

    internal partial class EstadoMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Estado")]
        public string Nombre { get; set; }

        [Display(Name = "Orden")]
        public virtual ICollection<Factura> Orden { get; set; }

        [Display(Name = "Orden detalle")]
        public virtual ICollection<OrdenDetalle> OrdenDetalle { get; set; }

        [Display(Name = "Producto")]
        public virtual ICollection<Producto> Producto { get; set; }

        [Display(Name = "Usuario")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }

    internal partial class EvaluacionMetadata
    {
        public int Id { get; set; }

        public Nullable<int> IdEvaluador { get; set; }
        public Nullable<int> IdEvaluado { get; set; }
        public Nullable<int> IdEscala { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El comentario es requerido")]
        [Display(Name = "Comentario")]
        public string Comentario { get; set; }

        [Required(ErrorMessage = "La escala es requerida")]
        [Display(Name = "Escala")]
        public virtual Escala Escala { get; set; }

        [Required(ErrorMessage = "Sus datos son requeridos")]
        [Display(Name = "Evaluador")]
        public virtual Usuario Usuario { get; set; }

        [Required(ErrorMessage = "Los datos de la persona ha evaluador son requeridos")]
        [Display(Name = "Evaluado")]
        public virtual Usuario Usuario1 { get; set; }
    }

    internal partial class FotoMetadata
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }

        [Display(Name = "Fotografía")]
        public byte[] Media { get; set; }

        [Display(Name = "Producto")]
        public virtual Producto Producto { get; set; }
    }

    internal partial class FacturaMetadata
    {
        public int IdFactura { get; set; }
        public Nullable<int> IdCuentaPago { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        [DataType(DataType.Date, ErrorMessage = "Fecha inválida")]
        [Display(Name = "Fecha de Facturación")]
        public System.DateTime Fecha { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IVA { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression(@"^[0-9]+(\,[0-9]{1,2})?$")]
        public Nullable<double> Total { get; set; }

        public Nullable<int> IdOrden { get; set; }

        public virtual CuentaPago CuentaPago { get; set; }
        public virtual Factura Orden { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    internal partial class OrdenMetadata
    {
        [Display(Name = "Número de orden")]
        public int IdOrden { get; set; }

        public Nullable<int> IdEstado { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        [DataType(DataType.Date, ErrorMessage = "Fecha inválida")]
        [Display(Name = "Fecha de inicio")]
        public Nullable<System.DateTime> FechaInicio { get; set; }


        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "El subtotal es requerido")]
        [RegularExpression(@"^[0-9]+(\,[0-9]{1,2})?$", ErrorMessage = "El subtotal solo acepta números con dos decimales")]
        public Nullable<double> SubTotal { get; set; }

        [Display(Name = "Estado")]
        public virtual Estado Estado { get; set; }


        [Display(Name = "Factura")]
        public virtual ICollection<Factura> Factura { get; set; }


        [Display(Name = "Orden Detalle")]
        public virtual ICollection<OrdenDetalle> OrdenDetalle { get; set; }
    }

    internal partial class OrdenDetalleMetadata
    {
        public int IdOrden { get; set; }
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La cantidad solo acepta números")]
        public Nullable<int> Cantidad { get; set; }

        public Nullable<int> IdEstado { get; set; }

        public Nullable<System.DateTime> FechaEntrega { get; set; }

        [Display(Name = "Estado")]
        public virtual Estado Estado { get; set; }

        [Display(Name = "Orden")]
        public virtual Factura Orden { get; set; }

        [Required(ErrorMessage = "Los productos son requeridos")]
        [Display(Name = "Produto")]
        public virtual Producto Producto { get; set; }
    }

    internal partial class ProductoMetadata
    {
        public int IdProducto { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del producto es requerido")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción es requerida")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }


        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "El precio es requerido")]
        [RegularExpression(@"^[0-9]+(\,[0-9]{1,2})?$", ErrorMessage = "El precio solo acepta números con dos decimales")]
        public Nullable<double> Precio { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La cantidad solo acepta números")]
        public Nullable<int> Cantidad { get; set; }
       
        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "La categoría es requerida")]
        public Nullable<int> IdCategoria { get; set; }

        [Display(Name = "Proveedor")]
        public Nullable<int> IdProveedor { get; set; }
        
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El estado es requerido")]
        public Nullable<int> IdEstado { get; set; }

        [Display(Name = "Ventas realizadas")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La cantidad solo acepta números")]
        public Nullable<int> VentasR { get; set; }

        [Display(Name = "Categoría")]
        public virtual Categoria Categoria { get; set; }

        [Display(Name = "Estado")]
        public virtual Estado Estado { get; set; }

        [Required(ErrorMessage = "La fotografía es requerida")]
        [Display(Name = "Fotografía")]
        public virtual ICollection<Foto> Foto { get; set; }

        [Display(Name = "Mensaje")]
        public virtual ICollection<Mensaje> Mensaje { get; set; }

        [Display(Name = "Orden detalle")]
        public virtual ICollection<OrdenDetalle> OrdenDetalle { get; set; }

        [Display(Name = "Proveedor")]
        public virtual Usuario Usuario { get; set; }
    }

    internal partial class TipoPagoMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Tipos de pagos")]
        public string Nombre { get; set; }

        [Display(Name = "Cuenta de pago")]
        public virtual ICollection<CuentaPago> CuentaPago { get; set; }
    }

    internal partial class TipoUsuarioMetadata
    {

        public int Id { get; set; }

        [Display(Name = "Tipos de usuarios")]
        public string Nombre { get; set; }

        [Display(Name = "Usuario")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }

    internal partial class UsuarioMetadata
    {
        public int Id { get; set; }


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

        
        [Required(AllowEmptyStrings = true, ErrorMessage = "El correo electrónico es requerido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Correo { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "La contraseña es requerida")]
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }


        public Nullable<int> IdEstado { get; set; }

        [Display(Name = "Fotografía")]
        public byte[] Foto { get; set; }

        [Required(ErrorMessage = "El género es requerido")]
        [Display(Name = "Género")]
        public string Genero { get; set; }

        [Display(Name = "Estado")]
        public virtual Estado Estado { get; set; }

        [Display(Name = "Tipo de usuario")]
        public virtual ICollection<TipoUsuario> TipoUsuario { get; set; }
    }

    internal partial class MensajeMetadata
    {

        public int Id { get; set; }
        public Nullable<int> IdProducto { get; set; }
        public string Mensaje1 { get; set; }
        public Nullable<int> IdUsuario { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Respuesta> Respuesta { get; set; }
    }
    internal partial class RespuestaMetadata
    {
        public int Id { get; set; }
        public Nullable<int> IdMensaje { get; set; }
        public string Respuesta1 { get; set; }
        public Nullable<int> IdProveedor { get; set; }

        public virtual Mensaje Mensaje { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

