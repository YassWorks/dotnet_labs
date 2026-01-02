using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;

    public GenreService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await _unitOfWork.Genres.GetAllAsync();
    }

    public async Task<Genre?> GetGenreByIdAsync(Guid id)
    {
        return await _unitOfWork.Genres.GetByIdAsync(id);
    }

    public async Task<Genre> AddGenreAsync(Genre genre)
    {
        await _unitOfWork.Genres.AddAsync(genre);
        await _unitOfWork.SaveChangesAsync();
        return genre;
    }

    public async Task UpdateGenreAsync(Genre genre)
    {
        _unitOfWork.Genres.Update(genre);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteGenreAsync(Guid id)
    {
        var genre = await _unitOfWork.Genres.GetByIdAsync(id);
        if (genre != null)
        {
            _unitOfWork.Genres.Remove(genre);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<object>> GetTop3PopularGenresAsync()
    {
        return await _unitOfWork.Genres.GetTop3PopularGenresAsync();
    }

    public async Task<bool> GenreExistsAsync(Guid id)
    {
        return await _unitOfWork.Genres.AnyAsync(g => g.Id == id);
    }

    public async Task<bool> HasMoviesAsync(Guid genreId)
    {
        return await _unitOfWork.Genres.HasMoviesAsync(genreId);
    }
}
