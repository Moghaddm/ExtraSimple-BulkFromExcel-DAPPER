using System.Text;
using Dapper;
using Application.Domain;
using Application.Domain.IRepositories;
using Application.Infrastructure.Persistence;

namespace Application.Infrastructure;

public class CustomerRepository : ICustomerRepository
{
    private readonly DapperContext _context;

    public CustomerRepository(DapperContext context) => _context = context;

    public async Task<int> BulkInsert(List<Customer> customers)
    {
        StringBuilder query = new StringBuilder();
        lock (query)
        {
            query.Append("INSERT INTO CUSTOMERS(FIRSTNAME,LASTNAME,AGE,MOBILE) VALUES");
        }
        int i = 0;
        do
        {
            query.Append(
                $"('{customers[i].FirstName}','{customers[i].LastName}',{customers[i].Age},'{customers[i].MobileNumber}'),"
            );
            i++;
        } while (i < customers.Count - 1);
        query.Length--;
        query.Append(";");
        using (var connection = _context.CreateConnection())
        {
            return await connection.ExecuteAsync(query.ToString());
        }
    }
}
