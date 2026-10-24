namespace ProgramasFacpya.Api.Data;
using Microsoft.Data.SqlClient;
using System.Data;
public class DbContext(IConfiguration configuration)
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")!;

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
