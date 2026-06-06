using System.ComponentModel.DataAnnotations;

namespace WebApplicationDistribuidoraApp.Models
{
    public class Producto
    {
        public Guid idProducto { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        public string clave { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de producto.")]
        public Guid? idTipoProducto { get; set; }

        [Range(0.01, double.MaxValue,ErrorMessage = "El precio de venta debe ser mayor a 0.")]
        public decimal? precioVenta { get; set; }
        public bool activo { get; set; }
    }
}
