using WebApplicationDistribuidoraApp.Models;

namespace WebApplicationDistribuidoraApp.ViewModels
{
    public class ProductoFiltroViewModel
    {
        public string? Clave { get; set; }

        public Guid? IdTipoProducto { get; set; }

        public List<Producto> Productos { get; set; } = new();

        public List<TiposProducto> TiposProducto { get; set; } = new();
    }
}
