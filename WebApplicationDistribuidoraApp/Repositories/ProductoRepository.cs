using WebApplicationDistribuidoraApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace WebApplicationDistribuidoraApp.Repositories
{
    public class ProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Producto> listarProductos(string clave, Guid? idTipoProducto, bool activo) {
            try
            {
                List<Producto> productos = new List<Producto>();
                using (SqlConnection conexion = new SqlConnection(_connectionString)) {
                    SqlCommand cmd = new SqlCommand("sp_listarProductos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@clave",clave);
                    cmd.Parameters.AddWithValue("@idTipoProducto", idTipoProducto);
                    cmd.Parameters.AddWithValue("@activo", activo);
                    conexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        productos.Add(new Producto
                        {
                            idProducto = (Guid)reader["idProducto"],
                            clave = reader["clave"].ToString(),
                            nombre = reader["nombre"].ToString(),
                            idTipoProducto = (Guid)reader["idTipoProducto"],
                            activo = (bool)reader["activo"],
                            precioVenta = reader["precioVenta"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["precioVenta"])
                        });
                    }
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener los productos -" + ex.Message);
            }
        }
        public Producto obtnerProductoPorId(Guid idProducto) {
            Producto producto = null;
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_obtenerProductoPorId", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    conexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) {
                        producto = new Producto
                        {
                            idProducto = (Guid)reader["idProducto"],
                            clave = reader["clave"].ToString(),
                            nombre = reader["nombre"].ToString(),
                            idTipoProducto = (Guid)reader["idTipoProducto"],
                            activo = (bool)reader["activo"],
                            precioVenta = Convert.ToDecimal(reader["precioVenta"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el producto -" + ex.Message);
            }
            return producto;
        }
        public void insertarProducto(Producto producto)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString)) {
                    SqlCommand cmd = new SqlCommand("sp_insertarProducto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@clave", producto.clave);
                    cmd.Parameters.AddWithValue("@nombre", producto.nombre);
                    cmd.Parameters.AddWithValue("@idTipoProducto", producto.idTipoProducto);
                    cmd.Parameters.AddWithValue("@activo", producto.activo);
                    cmd.Parameters.AddWithValue("@precioVenta", producto.precioVenta);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al insertar el producto, el producto no se inserto - " + ex.Message);
            }        
        }
        public void actualizarProducto(Producto producto)
        {
            try { 
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_actualizarProducto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProducto", producto.idProducto);
                    cmd.Parameters.AddWithValue("@clave", producto.clave);
                    cmd.Parameters.AddWithValue("@nombre", producto.nombre);
                    cmd.Parameters.AddWithValue("@idTipoProducto", producto.idTipoProducto);
                    cmd.Parameters.AddWithValue("@activo", producto.activo);
                    cmd.Parameters.AddWithValue("@precioVenta", producto.precioVenta);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar el producto - " + ex.Message );
            }
        }

        public void eliminarProducto(Guid idProducto) {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString)) {
                    SqlCommand cmd = new SqlCommand("sp_eliminarProducto",conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al eliminar el producto - " +ex.Message);
            }
        }

        public void agregarProveedorProducto(Guid idProducto, Guid idProveedor, string claveProveedor, decimal costo) {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString)) {
                    SqlCommand cmd = new SqlCommand("sp_agregarProveedorProducto",conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@claveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@costo", costo);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("Ocurrio un error al agregar proveedor al producto -" +ex.Message);
            }
        }

    }
}
