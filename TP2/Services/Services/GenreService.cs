using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class GenreService(IGenreRepository genreRepository) : IGenreService
{
    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await genreRepository.GetAllAsync();
    }

    public async Task<Genre?> GetGenreByIdAsync(Guid id)
    {
        return await genreRepository.GetByIdAsync(id);
    }

    public async Task<Genre> AddGenreAsync(Genre genre)
    {
        await genreRepository.AddAsync(genre);
        await genreRepository.SaveChangesAsync();
        return genre;
    }

    public async Task UpdateGenreAsync(Genre genre)
    {
        genreRepository.Update(genre);
        await genreRepository.SaveChangesAsync();
    }

    public async Task DeleteGenreAsync(Guid id)
    {
        var genre = await genreRepository.GetByIdAsync(id);
        if (genre != null)
        {
            genreRepository.Remove(genre);
            await genreRepository.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<object>> GetTop3PopularGenresAsync()
    {
        return await genreRepository.GetTop3PopularGenresAsync();
    }

    public async Task<bool> GenreExistsAsync(Guid id)
    {
        return await genreRepository.AnyAsync(g => g.Id == id);
    }

    public async Task<bool> HasMoviesAsync(Guid genreId)
    {
        return await genreRepository.HasMoviesAsync(genreId);
    }
}
