using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class MovieService(IMovieRepository movieRepository) : IMovieService
{
    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        return await movieRepository.GetAllWithGenreAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await movieRepository.GetByIdWithGenreAsync(id);
    }

    public async Task<Movie> AddMovieAsync(Movie movie)
    {
        await movieRepository.AddAsync(movie);
        await movieRepository.SaveChangesAsync();
        return movie;
    }

    public async Task UpdateMovieAsync(Movie movie)
    {
        movieRepository.Update(movie);
        await movieRepository.SaveChangesAsync();
    }

    public async Task DeleteMovieAsync(int id)
    {
        var movie = await movieRepository.GetByIdAsync(id);
        if (movie != null)
        {
            movieRepository.Remove(movie);
            await movieRepository.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Movie>> GetActionMoviesInStockAsync()
    {
        return await movieRepository.GetActionMoviesInStockAsync();
    }

    public async Task<IEnumerable<Movie>> GetMoviesOrderedByReleaseDateAndTitleAsync()
    {
        return await movieRepository.GetMoviesOrderedByReleaseDateAndTitleAsync();
    }

    public async Task<int> GetTotalMoviesCountAsync()
    {
        return await movieRepository.CountAsync();
    }

    public async Task<IEnumerable<object>> GetMoviesWithGenreAsync()
    {
        return await movieRepository.GetMoviesWithGenreAsync();
    }

    public async Task<bool> MovieExistsAsync(int id)
    {
        return await movieRepository.AnyAsync(m => m.Id == id);
    }
}
