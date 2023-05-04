using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Application.Infrastructure.Persistence;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("PostgreSQL");
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
