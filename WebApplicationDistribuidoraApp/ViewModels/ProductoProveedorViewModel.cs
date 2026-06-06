using System.ComponentModel.DataAnnotations;
using WebApplicationDistribuidoraApp.Models;

namespace WebApplicationDistribuidoraApp.ViewModels
{
    public class ProductoProveedorViewModel
    {
            public Guid idProductoProveedor { get; set; }
            public Guid idProducto { get; set; }

            [Required(ErrorMessage = "El proveedor es obligatorio.")]
            public Guid? idProveedor { get; set; }          

            [Required(ErrorMessage = "La clave es obligatorio.")]
            public string claveProveedor { get; set; }

            [Required(ErrorMessage = "El costo es obligatorio.")]
            [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor a 0.")]
            public decimal? costo { get; set; }             

            public bool esEdicionProveedor { get; set; }
            public IEnumerable<Proveedor> proveedoresDisponibles { get; set; }
        
    }
}
