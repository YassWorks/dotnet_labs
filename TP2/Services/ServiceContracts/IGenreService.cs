using TP2.Models;

namespace TP2.Services.ServiceContracts;

public interface IGenreService
{
    Task<IEnumerable<Genre>> GetAllGenresAsync();
    Task<Genre?> GetGenreByIdAsync(Guid id);
    Task<Genre> AddGenreAsync(Genre genre);
    Task UpdateGenreAsync(Genre genre);
    Task DeleteGenreAsync(Guid id);
    Task<IEnumerable<object>> GetTop3PopularGenresAsync();
    Task<bool> GenreExistsAsync(Guid id);
    Task<bool> HasMoviesAsync(Guid genreId);
}
