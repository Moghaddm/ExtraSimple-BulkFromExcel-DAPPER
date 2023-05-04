using Application.Domain;

namespace Application.Domain.IRepositories;

public interface ICustomerRepository
{
    Task<int> BulkInsert(List<Customer> customers);
}
