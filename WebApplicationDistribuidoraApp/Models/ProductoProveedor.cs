using System.ComponentModel.DataAnnotations;

namespace WebApplicationDistribuidoraApp.Models
{
    public class ProductoProveedor
    {
        public Guid idProductoProveedor { get; set; }  
        public Guid idProducto { get; set; }
        [Required(ErrorMessage = "El proveedor es obligatorio.")]
        public Guid idProveedor { get; set; }
        [Required(ErrorMessage = "La clave  es obligatorio.")]
        public string claveProveedor { get; set; }
        public string nombreProveedor { get; set; }
        [Required(ErrorMessage = "El costo es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor a 0.")]
        public decimal costo { get; set; }
    }
}
