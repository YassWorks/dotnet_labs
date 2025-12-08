using TP2.Models;

namespace TP2.Services.ServiceContracts;

public interface IMovieService
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<Movie?> GetMovieByIdAsync(int id);
    Task<Movie> AddMovieAsync(Movie movie);
    Task UpdateMovieAsync(Movie movie);
    Task DeleteMovieAsync(int id);
    Task<IEnumerable<Movie>> GetActionMoviesInStockAsync();
    Task<IEnumerable<Movie>> GetMoviesOrderedByReleaseDateAndTitleAsync();
    Task<int> GetTotalMoviesCountAsync();
    Task<IEnumerable<object>> GetMoviesWithGenreAsync();
    Task<bool> MovieExistsAsync(int id);
}
