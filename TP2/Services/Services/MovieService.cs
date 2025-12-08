using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        return await _movieRepository.GetAllWithGenreAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await _movieRepository.GetByIdWithGenreAsync(id);
    }

    public async Task<Movie> AddMovieAsync(Movie movie)
    {
        await _movieRepository.AddAsync(movie);
        await _movieRepository.SaveChangesAsync();
        return movie;
    }

    public async Task UpdateMovieAsync(Movie movie)
    {
        _movieRepository.Update(movie);
        await _movieRepository.SaveChangesAsync();
    }

    public async Task DeleteMovieAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie != null)
        {
            _movieRepository.Remove(movie);
            await _movieRepository.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Movie>> GetActionMoviesInStockAsync()
    {
        return await _movieRepository.GetActionMoviesInStockAsync();
    }

    public async Task<IEnumerable<Movie>> GetMoviesOrderedByReleaseDateAndTitleAsync()
    {
        return await _movieRepository.GetMoviesOrderedByReleaseDateAndTitleAsync();
    }

    public async Task<int> GetTotalMoviesCountAsync()
    {
        return await _movieRepository.CountAsync();
    }

    public async Task<IEnumerable<object>> GetMoviesWithGenreAsync()
    {
        return await _movieRepository.GetMoviesWithGenreAsync();
    }

    public async Task<bool> MovieExistsAsync(int id)
    {
        return await _movieRepository.AnyAsync(m => m.Id == id);
    }
}
