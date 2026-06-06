using WebApplicationDistribuidoraApp.Models;
using WebApplicationDistribuidoraApp.Repositories;

namespace WebApplicationDistribuidoraApp.Services
{
    public class ProductoProveedorService
    {
        
            private readonly ProductoProveedorRepository _productoProveedorRepository;

            public ProductoProveedorService(ProductoProveedorRepository productoProveedorRepository)
            {
            _productoProveedorRepository = productoProveedorRepository;
            }

            public List<ProductoProveedor> listarProveedoresPorProducto(Guid idProducto)
            {
                return _productoProveedorRepository.listarProveedoresPorProducto(idProducto);
            }


    }
}
