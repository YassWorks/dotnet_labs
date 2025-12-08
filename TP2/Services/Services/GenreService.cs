using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await _genreRepository.GetAllAsync();
    }

    public async Task<Genre?> GetGenreByIdAsync(Guid id)
    {
        return await _genreRepository.GetByIdAsync(id);
    }

    public async Task<Genre> AddGenreAsync(Genre genre)
    {
        await _genreRepository.AddAsync(genre);
        await _genreRepository.SaveChangesAsync();
        return genre;
    }

    public async Task UpdateGenreAsync(Genre genre)
    {
        _genreRepository.Update(genre);
        await _genreRepository.SaveChangesAsync();
    }

    public async Task DeleteGenreAsync(Guid id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre != null)
        {
            _genreRepository.Remove(genre);
            await _genreRepository.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<object>> GetTop3PopularGenresAsync()
    {
        return await _genreRepository.GetTop3PopularGenresAsync();
    }

    public async Task<bool> GenreExistsAsync(Guid id)
    {
        return await _genreRepository.AnyAsync(g => g.Id == id);
    }

    public async Task<bool> HasMoviesAsync(Guid genreId)
    {
        return await _genreRepository.HasMoviesAsync(genreId);
    }
}
