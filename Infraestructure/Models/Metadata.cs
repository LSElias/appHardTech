using System;
using System.Collections.Generic;
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

        [Display(Name = "Productos")]

        public virtual ICollection<Producto> Producto { get; set; }

    }

    internal partial class CuentaPago
    {
        public int Id { get; set; }
        public Nullable<int> IdTipoPago { get; set; }

        [Display(Name = "Número de cuenta")]
        public string NumCuenta { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        public string FechaExp { get; set; }
        public string CodSeguridad { get; set; }
        public Nullable<int> IdUsuario { get; set; }

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
        public Nullable<int> IdUsuario { get; set; }

        [Display(Name = "Provincia")]
        public string Provincia { get; set; }

        [Display(Name = "Cantón")]
        public string Canton { get; set; }

        [Display(Name = "Distrito")]
        public string Distrito { get; set; }

        [Display(Name = "Dirección exacta")]
        public string DireccionExacta { get; set; }

        [Display(Name = "Usuario")]
        public virtual Usuario Usuario { get; set; }
    }

    internal partial class EstadoMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Orden")]
        public virtual ICollection<Orden> Orden { get; set; }

        [Display(Name = "Orden Detalle")]
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

        [Display(Name = "Comentario")]
        public string Comentario { get; set; }

        [Display(Name = "Escala")]
        public virtual Escala Escala { get; set; }

        [Display(Name = "Evaluador")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Evaluado")]
        public virtual Usuario Usuario1 { get; set; }
    }

    internal partial class FotoMetadata
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }

        [Display(Name = "Foto")]
        public byte[] Media { get; set; }

        [Display(Name = "Producto")]
        public virtual Producto Producto { get; set; }
    }

    internal partial class OrdenMetadata
    {
        public int Id { get; set; }

        public Nullable<int> Estado { get; set; }

        [Display(Name = "Fecha de inicio")]
        public Nullable<System.DateTime> FechaInicio { get; set; }

        [Display(Name = "Subtotal")]
        public Nullable<double> SubTotal { get; set; }

        [Display(Name = "Estado")]
        public virtual Estado Estado1 { get; set; }

        [Display(Name = "Factura")]
        public virtual ICollection<Factura> Factura { get; set; }


        [Display(Name = "Orden Detalle")]
        public virtual ICollection<OrdenDetalle> OrdenDetalle { get; set; }
    }

    internal partial class OrdenDetalleMetadata
    {
        public int IdOrden { get; set; }
        public int idProducto { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<int> Estado { get; set; }

        [Display(Name = "Fecha de entrega")]
        public Nullable<System.DateTime> FechaEntrega { get; set; }

        [Display(Name = "Estado")]
        public virtual Estado Estado1 { get; set; }

        [Display(Name = "Orden")]
        public virtual Orden Orden { get; set; }

        [Display(Name = "Produto")]
        public virtual Producto Producto { get; set; }
    }

    internal partial class ProductoMetadata
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<double> Precio { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public Nullable<int> Estado { get; set; }
        public Nullable<int> VentasR { get; set; }

        [Display(Name = "Categoría")]
        public virtual Categoria Categoria { get; set; }

        [Display(Name = "Estado")]
        public virtual Estado Estado1 { get; set; }


        [Display(Name = "Foto")]
        public virtual ICollection<Foto> Foto { get; set; }

        [Display(Name = "Mensaje")]
        public virtual ICollection<Mensaje> Mensaje { get; set; }

        [Display(Name = "Orden Detalle")]
        public virtual ICollection<OrdenDetalle> OrdenDetalle { get; set; }

        [Display(Name = "Proveedor")]
        public virtual Usuario Usuario { get; set; }
    }

    internal partial class TipoPagoMetadata
    {

        public int Id { get; set; }
        public string Nombre { get; set; }

        [Display(Name = "Cuenta de pago")]
        public virtual ICollection<CuentaPago> CuentaPago { get; set; }
    }

    internal partial class TipoUsuarioMetadata
    {

        public int Id { get; set; }
        public string Nombre { get; set; }

        [Display(Name = "Usuario")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }

    internal partial class UsuarioMetadata
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public Nullable<int> Estado { get; set; }
        public byte[] Foto { get; set; }
        public string Genero { get; set; }

        [Display(Name = "Cuenta de pago")]
        public virtual ICollection<CuentaPago> CuentaPago { get; set; }

        [Display(Name = "Direccion")]
        public virtual ICollection<Direccion> Direccion { get; set; }

        [Display(Name = "Estado")]
        public virtual Estado Estado1 { get; set; }

        [Display(Name = "Tipo de usuario")]
        public virtual ICollection<TipoUsuario> TipoUsuario { get; set; }
    }
}
