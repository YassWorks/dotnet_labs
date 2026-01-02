using Microsoft.EntityFrameworkCore;
using TP2.Models;
using TP2.Data;

namespace TP2.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllWithMembershipTypeAsync()
    {
        return await _context.Customers!.Include(c => c.MembershipType).ToListAsync();
    }

    public async Task<Customer?> GetByIdWithMembershipTypeAsync(int id)
    {
        return await _context.Customers!.Include(c => c.MembershipType).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Customer>> GetSubscribedCustomersWithDiscountAsync()
    {
        var results = from customer in _context.Customers!
            join membership in _context.MembershipTypes! on customer.MembershipTypeId equals membership.Id
            where customer.IsSubscribed && membership.DiscountRate > 0.10m
            select customer;

        return await results.Include(c => c.MembershipType).ToListAsync();
    }
}