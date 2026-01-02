using TP2.Data;
using TP2.Models;

namespace TP2.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private ICustomerRepository? _customerRepository;
    private IMovieRepository? _movieRepository;
    private IGenreRepository? _genreRepository;
    private IGenericRepository<MembershipType>? _membershipTypeRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ICustomerRepository Customers
    {
        get { return _customerRepository ??= new CustomerRepository(_context); }
    }

    public IMovieRepository Movies
    {
        get { return _movieRepository ??= new MovieRepository(_context); }
    }

    public IGenreRepository Genres
    {
        get { return _genreRepository ??= new GenreRepository(_context); }
    }

    public IGenericRepository<MembershipType> MembershipTypes
    {
        get { return _membershipTypeRepository ??= new GenericRepository<MembershipType>(_context); }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
