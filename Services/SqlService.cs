using System.Data;
//using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Mercury.Services
{
    public class SqlService
    {
        private readonly string _connectionString;

        public SqlService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> ExecuteStoredProcedureAsync(String tableName, int returnSID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("dbo.newsid", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TableName", tableName);
                    command.Parameters.AddWithValue("@ReturnSID", returnSID);

                    var result = await command.ExecuteScalarAsync(); // or ExecuteNonQueryAsync, depending on your needs

                    return Convert.ToInt32(result);
                }
            }
        }
    }
}
