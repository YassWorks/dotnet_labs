using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _customerRepository.GetAllWithMembershipTypeAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _customerRepository.GetByIdWithMembershipTypeAsync(id);
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveChangesAsync();
        return customer;
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        _customerRepository.Update(customer);
        await _customerRepository.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer != null)
        {
            _customerRepository.Remove(customer);
            await _customerRepository.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Customer>> GetSubscribedCustomersWithDiscountAsync()
    {
        return await _customerRepository.GetSubscribedCustomersWithDiscountAsync();
    }

    public async Task<bool> CustomerExistsAsync(int id)
    {
        return await _customerRepository.AnyAsync(c => c.Id == id);
    }
}
