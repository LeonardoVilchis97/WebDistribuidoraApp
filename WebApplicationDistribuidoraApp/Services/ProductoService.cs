using WebApplicationDistribuidoraApp.Models;
using WebApplicationDistribuidoraApp.Repositories;

namespace WebApplicationDistribuidoraApp.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ProductoRepository _productoRepository;
        public ProductoService(ProductoRepository productoRepository) {
            _productoRepository = productoRepository;
        }

      

        public IEnumerable<Producto> listarProductos(string clave, Guid? idTipoProducto)
        {
            if (clave != null && clave.Trim() == "")
                throw new Exception("La clave del producto no puede estar vacia. ");
            return _productoRepository.listarProductos(clave, idTipoProducto,true);
        }

        public Producto obtenerProductoPorId(Guid idProducto)
        {
            return _productoRepository.obtnerProductoPorId(idProducto);
        }
        public void insertarProducto(Producto producto)
        {
            if(producto.precioVenta<=0)
                throw new Exception("El producto no puede ser meno o igual a 0.");
            _productoRepository.insertarProducto(producto);
        }
        public void actualizarProducto(Producto producto)
        {
            _productoRepository.actualizarProducto(producto);
        }

        public void agregarProveedorProducto(Guid idProducto, Guid idProveedor, string claveProveedor, decimal costo)
        {
            if (costo <= 0)
                throw new Exception("El costo debe de ser mayor a 0.");

            _productoRepository.agregarProveedorProducto(idProducto, idProveedor, claveProveedor, costo);
        }

        public void eliminarProducto(Guid idProducto)
        {
            try
            {
                _productoRepository.eliminarProducto(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al eliminar el producto - " + ex.Message, ex); 
            }
        }
    }
}
