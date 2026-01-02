using TP2.Models;

namespace TP2.Repositories;

public interface IUnitOfWork : IDisposable
{
    ICustomerRepository Customers { get; }
    IMovieRepository Movies { get; }
    IGenreRepository Genres { get; }
    IGenericRepository<MembershipType> MembershipTypes { get; }

    Task<int> SaveChangesAsync();
}
