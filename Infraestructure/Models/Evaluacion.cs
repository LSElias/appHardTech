//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(EvaluacionMetadata))]


    public partial class Evaluacion
    {
        public int Id { get; set; }
        public Nullable<int> IdEvaluador { get; set; }
        public Nullable<int> IdEvaluado { get; set; }
        public Nullable<int> IdEscala { get; set; }
        public string Comentario { get; set; }
    
        public virtual Escala Escala { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
    }
}
