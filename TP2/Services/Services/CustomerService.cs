using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await customerRepository.GetAllWithMembershipTypeAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await customerRepository.GetByIdWithMembershipTypeAsync(id);
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        await customerRepository.AddAsync(customer);
        await customerRepository.SaveChangesAsync();
        return customer;
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        customerRepository.Update(customer);
        await customerRepository.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await customerRepository.GetByIdAsync(id);
        if (customer != null)
        {
            customerRepository.Remove(customer);
            await customerRepository.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Customer>> GetSubscribedCustomersWithDiscountAsync()
    {
        return await customerRepository.GetSubscribedCustomersWithDiscountAsync();
    }

    public async Task<bool> CustomerExistsAsync(int id)
    {
        return await customerRepository.AnyAsync(c => c.Id == id);
    }
}
