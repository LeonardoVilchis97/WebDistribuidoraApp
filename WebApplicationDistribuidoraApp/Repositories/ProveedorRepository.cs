using System.Data;
using System.Data.SqlClient;
using WebApplicationDistribuidoraApp.Models;

namespace WebApplicationDistribuidoraApp.Repositories
{
    public class ProveedorRepository
    {
        private readonly string _connectionString;

        public ProveedorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Proveedor> listarProveedores()
        {
            try
            {
                List<Proveedor> proveedores = new List<Proveedor>();
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_listarProveedores", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        proveedores.Add(new Proveedor
                        {
                            idProveedor = (Guid)reader["idProveedor"],
                            nombre = reader["nombre"].ToString(),
                            descripcion = reader["descripcion"].ToString()
                        }) ;
                    }
                }
                return proveedores;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener los proveedores -" + ex.Message);
            }
        }
    }
}
