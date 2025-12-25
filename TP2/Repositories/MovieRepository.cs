using Microsoft.EntityFrameworkCore;
using TP2.Models;

namespace TP2.Repositories;

public class MovieRepository(ApplicationDbContext context) : GenericRepository<Movie>(context), IMovieRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Movie>> GetAllWithGenreAsync()
    {
        return await _context.Movies!.Include(m => m.Genre).ToListAsync();
    }

    public async Task<Movie?> GetByIdWithGenreAsync(int id)
    {
        return await _context.Movies!.Include(m => m.Genre).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Movie>> GetActionMoviesInStockAsync()
    {
        var actionMovies = from movie in _context.Movies!
                           join genre in _context.Genres! on movie.GenreId equals genre.Id
                           where genre.Name == "Action" && movie.Stock > 0
                           select movie;

        return await actionMovies.Include(m => m.Genre).ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetMoviesOrderedByReleaseDateAndTitleAsync()
    {
        var orderedMovies = from movie in _context.Movies!
                            orderby movie.ReleaseDate, movie.Name
                            select movie;

        return await orderedMovies.Include(m => m.Genre).ToListAsync();
    }

    public async Task<IEnumerable<object>> GetMoviesWithGenreAsync()
    {
        var moviesWithGenre = from movie in _context.Movies!
                              join genre in _context.Genres! on movie.GenreId equals genre.Id
                              select new
                              {
                                  MovieTitle = movie.Name,
                                  GenreName = genre.Name
                              };

        return await moviesWithGenre.ToListAsync();
    }
}
