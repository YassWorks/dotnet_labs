using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class MovieService : IMovieService
{
    private readonly IUnitOfWork _unitOfWork;

    public MovieService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        return await _unitOfWork.Movies.GetAllWithGenreAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await _unitOfWork.Movies.GetByIdWithGenreAsync(id);
    }

    public async Task<Movie> AddMovieAsync(Movie movie)
    {
        await _unitOfWork.Movies.AddAsync(movie);
        await _unitOfWork.SaveChangesAsync();
        return movie;
    }

    public async Task UpdateMovieAsync(Movie movie)
    {
        _unitOfWork.Movies.Update(movie);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteMovieAsync(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);
        if (movie != null)
        {
            _unitOfWork.Movies.Remove(movie);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Movie>> GetActionMoviesInStockAsync()
    {
        return await _unitOfWork.Movies.GetActionMoviesInStockAsync();
    }

    public async Task<IEnumerable<Movie>> GetMoviesOrderedByReleaseDateAndTitleAsync()
    {
        return await _unitOfWork.Movies.GetMoviesOrderedByReleaseDateAndTitleAsync();
    }

    public async Task<int> GetTotalMoviesCountAsync()
    {
        return await _unitOfWork.Movies.CountAsync();
    }

    public async Task<IEnumerable<object>> GetMoviesWithGenreAsync()
    {
        return await _unitOfWork.Movies.GetMoviesWithGenreAsync();
    }

    public async Task<bool> MovieExistsAsync(int id)
    {
        return await _unitOfWork.Movies.AnyAsync(m => m.Id == id);
    }
}
