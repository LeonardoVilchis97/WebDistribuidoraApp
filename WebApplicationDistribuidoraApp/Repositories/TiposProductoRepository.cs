using System.Data;
using System.Data.SqlClient;
using WebApplicationDistribuidoraApp.Models;

namespace WebApplicationDistribuidoraApp.Repositories
{
    public class TiposProductoRepository
    {
        private readonly string _connectionString;

        public TiposProductoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<TiposProducto> obtenerTiposProducto()
        {
            List<TiposProducto> tiposProductos = new List<TiposProducto>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_listarTiposProducto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tiposProductos.Add(new TiposProducto
                            {
                                idTipoProducto = (Guid)reader["idTipoProducto"],
                                nombre = reader["nombre"].ToString(),
                                descripcion = reader["descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al obtener los tipos de productos."+ ex.Message);
            }
            return tiposProductos;
        }
    }
}
