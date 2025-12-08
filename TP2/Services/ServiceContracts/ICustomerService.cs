using TP2.Models;

namespace TP2.Services.ServiceContracts;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer> AddCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(int id);
    Task<IEnumerable<Customer>> GetSubscribedCustomersWithDiscountAsync();
    Task<bool> CustomerExistsAsync(int id);
}
