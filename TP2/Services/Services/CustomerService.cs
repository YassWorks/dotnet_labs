using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _unitOfWork.Customers.GetAllWithMembershipTypeAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _unitOfWork.Customers.GetByIdWithMembershipTypeAsync(id);
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();
        return customer;
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        _unitOfWork.Customers.Update(customer);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id);
        if (customer != null)
        {
            _unitOfWork.Customers.Remove(customer);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Customer>> GetSubscribedCustomersWithDiscountAsync()
    {
        return await _unitOfWork.Customers.GetSubscribedCustomersWithDiscountAsync();
    }

    public async Task<bool> CustomerExistsAsync(int id)
    {
        return await _unitOfWork.Customers.AnyAsync(c => c.Id == id);
    }
}
