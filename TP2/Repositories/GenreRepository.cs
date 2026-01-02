using Microsoft.EntityFrameworkCore;
using TP2.Models;
using TP2.Data;

namespace TP2.Repositories;

public class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    private readonly ApplicationDbContext _context;

    public GenreRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> GetTop3PopularGenresAsync()
    {
        var top3Genres = await (from genre in _context.Genres!
                join movie in _context.Movies! on genre.Id equals movie.GenreId into movieGroup
                select new
                {
                    GenreName = genre.Name,
                    MovieCount = movieGroup.Count()
                })
            .OrderByDescending(g => g.MovieCount)
            .Take(3)
            .ToListAsync();

        return top3Genres;
    }

    public async Task<bool> HasMoviesAsync(Guid genreId)
    {
        return await _context.Movies!.AnyAsync(m => m.GenreId == genreId);
    }
}