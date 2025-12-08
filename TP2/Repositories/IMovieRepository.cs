using TP2.Models;

namespace TP2.Repositories;

public interface IMovieRepository : IGenericRepository<Movie>
{
    Task<IEnumerable<Movie>> GetAllWithGenreAsync();
    Task<Movie?> GetByIdWithGenreAsync(int id);
    Task<IEnumerable<Movie>> GetActionMoviesInStockAsync();
    Task<IEnumerable<Movie>> GetMoviesOrderedByReleaseDateAndTitleAsync();
    Task<IEnumerable<object>> GetMoviesWithGenreAsync();
}
