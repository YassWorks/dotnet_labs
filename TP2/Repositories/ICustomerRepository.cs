using TP2.Models;

namespace TP2.Repositories;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<IEnumerable<Customer>> GetAllWithMembershipTypeAsync();
    Task<Customer?> GetByIdWithMembershipTypeAsync(int id);
    Task<IEnumerable<Customer>> GetSubscribedCustomersWithDiscountAsync();
}
