using Microsoft.AspNetCore.Mvc;
using WebApplicationDistribuidoraApp.Models;
using WebApplicationDistribuidoraApp.Repositories;
using WebApplicationDistribuidoraApp.Services;
using WebApplicationDistribuidoraApp.ViewModels;

namespace WebApplicationDistribuidoraApp.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _iProductoService;
        private readonly TiposProductoRepository _tiposProductoRepository;
        private readonly ProductoProveedorRepository _productoProveedorRepository;
        private readonly ProveedorRepository _proveedorRepository;
        public ProductoController(IProductoService productoService, TiposProductoRepository tiposProductoRepository, ProductoProveedorRepository productoProveedorService,ProveedorRepository proveedorRepository)
        {
            _iProductoService = productoService;
            _tiposProductoRepository = tiposProductoRepository;
            _productoProveedorRepository = productoProveedorService;
            _proveedorRepository = proveedorRepository;
        }

        [HttpGet]
        public IActionResult Index(string? clave, Guid? idTipoProducto)
        {
            var vm = new ProductoFiltroViewModel();
            vm.Clave = clave;
            vm.IdTipoProducto = idTipoProducto;
            vm.TiposProducto = _tiposProductoRepository.obtenerTiposProducto();
            vm.Productos = _iProductoService.listarProductos(clave, idTipoProducto).ToList();
            return View(vm);
        }


        [HttpGet]
        public IActionResult Editar(Guid idProducto)
        {
            var tipos = _tiposProductoRepository.obtenerTiposProducto();
            ViewBag.TiposProducto = tipos;

            Producto producto;
            List<ProductoProveedor> proveedoresProducto;

            if (idProducto == Guid.Empty)
            {
                producto = new Producto();
                proveedoresProducto = new List<ProductoProveedor>();
            }
            else
            {
                producto = _iProductoService.obtenerProductoPorId(idProducto);
                proveedoresProducto = _productoProveedorRepository.listarProveedoresPorProducto(idProducto);
            }

            ViewData["ProveedoresDisponibles"] = _proveedorRepository.listarProveedores();
            ViewData["ProveedoresProducto"] = proveedoresProducto;

            return View(producto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insertar(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TiposProducto = _tiposProductoRepository.obtenerTiposProducto();
                return View("Editar", producto);
            }

            if (producto.idProducto == Guid.Empty)
                _iProductoService.insertarProducto(producto);
            else
                _iProductoService.actualizarProducto(producto);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Guid idProducto)
        {
            try
            {
                _iProductoService.eliminarProducto(idProducto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "No se puede eliminar el producto porque está siendo utilizado en otros registros.";
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public IActionResult AgregarProductoProveedor(ProductoProveedorViewModel model)
        {
            ModelState.Remove("proveedoresDisponibles");
            ModelState.Remove("esEdicionProveedor");
            ModelState.Remove("idProductoProveedor");

            if (!ModelState.IsValid)
            {
                model.proveedoresDisponibles = _proveedorRepository.listarProveedores();
                return PartialView("_AgregarProveedorModal", model);
            }

            _iProductoService.agregarProveedorProducto(
                model.idProducto,
                model.idProveedor.Value,
                model.claveProveedor,
                model.costo.Value
            );

            return Json(new { success = true }); // ✅ cambiado
        }

        [HttpGet]
        public IActionResult AgregarProveedor(Guid idProducto)
        {
            var proveedores = _proveedorRepository.listarProveedores() ?? new List<Proveedor>();
            var vm = new ProductoProveedorViewModel
            {
                idProducto = idProducto,
                esEdicionProveedor = false,
                proveedoresDisponibles = proveedores
            };
            return PartialView("_AgregarProveedorModal", vm);
        }

        [HttpGet]
        public IActionResult EditarProductoProveedor(Guid idProductoProveedor)
        {
            var proveedorProducto = _productoProveedorRepository.obtenerProductoProveedorPorId(idProductoProveedor);
            var proveedores = _proveedorRepository.listarProveedores() ?? new List<Proveedor>();

            var vm = new ProductoProveedorViewModel
            {
                idProductoProveedor = proveedorProducto.idProductoProveedor,
                idProducto = proveedorProducto.idProducto,
                idProveedor = proveedorProducto.idProveedor,
                claveProveedor = proveedorProducto.claveProveedor,
                costo = proveedorProducto.costo,
                esEdicionProveedor = true,
                proveedoresDisponibles = proveedores
            };

            return PartialView("_AgregarProveedorModal", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarProductoProveedor(ProductoProveedorViewModel vm)
        {
            ModelState.Remove("proveedoresDisponibles");

            if (!ModelState.IsValid)
            {
                vm.proveedoresDisponibles = _proveedorRepository.listarProveedores();
                vm.esEdicionProveedor = true;
                return PartialView("_AgregarProveedorModal", vm);
            }

            ProductoProveedor productoProveedor = new ProductoProveedor();
            productoProveedor.idProducto = vm.idProducto;
            productoProveedor.idProveedor = vm.idProveedor.Value;
            productoProveedor.claveProveedor = vm.claveProveedor;
            productoProveedor.costo = vm.costo.Value;
            productoProveedor.idProductoProveedor = vm.idProductoProveedor;

            _productoProveedorRepository.actualizarProductoProveedor(productoProveedor);

            return Json(new { success = true }); // ✅ cambiado
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarProveedor(Guid idProductoProveedor)
        {
            if (idProductoProveedor == Guid.Empty)
                return BadRequest("Id inválido");
            var pp = _productoProveedorRepository.obtenerProductoProveedorPorId(idProductoProveedor);
            var idProducto = pp.idProducto; 
            _productoProveedorRepository.eliminarProductoProveedor(idProductoProveedor);
            return RedirectToAction(nameof(Editar), new { idProducto }); 
        }



    }
}
