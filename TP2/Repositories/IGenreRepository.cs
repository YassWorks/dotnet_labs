using TP2.Models;

namespace TP2.Repositories;

public interface IGenreRepository : IGenericRepository<Genre>
{
    Task<IEnumerable<object>> GetTop3PopularGenresAsync();
    Task<bool> HasMoviesAsync(Guid genreId);
}
