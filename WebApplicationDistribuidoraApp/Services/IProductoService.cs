using WebApplicationDistribuidoraApp.Models;

namespace WebApplicationDistribuidoraApp.Services
{
    public interface IProductoService
    {
        IEnumerable<Producto> listarProductos(string clave, Guid? idProducto);
        Producto obtenerProductoPorId(Guid idProducto);
        void insertarProducto(Producto producto);
        void actualizarProducto(Producto producto);
        void eliminarProducto(Guid producto);
        void agregarProveedorProducto(Guid idProducto, Guid idProveedor, string claveProveedor, decimal costo);
    }
}
