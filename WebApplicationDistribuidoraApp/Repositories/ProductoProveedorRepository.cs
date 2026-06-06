using System.Data;
using System.Data.SqlClient;
using WebApplicationDistribuidoraApp.Models;

namespace WebApplicationDistribuidoraApp.Repositories
{
    public class ProductoProveedorRepository
    {
        private readonly string _connectionString;

        public ProductoProveedorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ProductoProveedor> listarProveedoresPorProducto(Guid idProducto)
        {
            var proveedores = new List<ProductoProveedor>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_listarProveedoresPorProducto", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idProducto", idProducto);
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                proveedores.Add(new ProductoProveedor
                                {
                                    idProductoProveedor = (Guid)reader["idProductoProveedor"],
                                    idProducto = (Guid)reader["idProductoProveedor"],
                                    idProveedor = (Guid)reader["idProductoProveedor"],
                                    claveProveedor = reader["claveProveedor"].ToString(),
                                    nombreProveedor = reader["nombreProveedor"].ToString(),
                                    costo = reader.GetDecimal(5)
                                });
                            }
                        }
                    }
                }
                return proveedores;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el listado de los productos por proveedor -" + ex.Message);
            }

        }


        public ProductoProveedor obtenerProductoProveedorPorId(Guid idProductoProveedor) {
            ProductoProveedor productoProveedor = null;
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_obtenerProductoProveedorPorId", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProductoProveedor", idProductoProveedor);
                    conexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        productoProveedor = new ProductoProveedor
                        {
                            idProductoProveedor = (Guid)reader["idProductoProveedor"],
                            idProducto = (Guid)reader["idProducto"],
                            idProveedor = (Guid)reader["idProveedor"],
                            claveProveedor = reader["claveProveedor"].ToString(),
                            costo = (decimal)reader["costo"]

                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el producto del proveedor -" + ex.Message);
            }
            return productoProveedor;
        }
        public void actualizarProductoProveedor(ProductoProveedor productoProveedor)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_actualizarProductoProveedor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idProductoProveedor", productoProveedor.idProductoProveedor);
                    cmd.Parameters.AddWithValue("@idProducto", productoProveedor.idProducto);
                    cmd.Parameters.AddWithValue("@idProveedor", productoProveedor.idProveedor);
                    cmd.Parameters.AddWithValue("@claveProveedor", productoProveedor.claveProveedor);
                    cmd.Parameters.AddWithValue("@costo", productoProveedor.costo);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al actualizar el producto proveedor - " + ex.Message);
            }
        }
        public void eliminarProductoProveedor(Guid idProductoProveedor)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminarProductoProveedor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProductoProveedor", idProductoProveedor);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al eliminar el producto proveedor - " + ex.Message);
            }
        }


    }
}
