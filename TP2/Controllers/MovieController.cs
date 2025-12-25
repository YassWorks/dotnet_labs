using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2.Models;
using TP2.Services.ServiceContracts;

namespace TP2.Controllers;

public class MovieController : Controller
{
    private readonly IMovieService _movieService;
    private readonly IGenreService _genreService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MovieController(IMovieService movieService, IGenreService genreService, IWebHostEnvironment webHostEnvironment)
    {
        _movieService = movieService;
        _genreService = genreService;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: Movie/
    public async Task<IActionResult> Index(string sortOrder, int? pageNumber)
    {
        // Sort parameters
        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["GenreSortParm"] = sortOrder == "Genre" ? "genre_desc" : "Genre";
        ViewData["CurrentSort"] = sortOrder;

        var movies = (await _movieService.GetAllMoviesAsync()).AsQueryable();

        // Dynamic sorting
        movies = sortOrder switch
        {
            "name_desc" => movies.OrderByDescending(m => m.Name),
            "Genre" => movies.OrderBy(m => m.Genre!.Name),
            "genre_desc" => movies.OrderByDescending(m => m.Genre!.Name),
            _ => movies.OrderBy(m => m.Name)
        };

        // Simple pagination
        int pageSize = 5;
        ViewData["PageNumber"] = pageNumber ?? 1;
        ViewData["HasPreviousPage"] = pageNumber > 1;
        ViewData["HasNextPage"] = movies.Skip((pageNumber ?? 1) * pageSize).Any();

        var paginatedMovies = movies
            .Skip(((pageNumber ?? 1) - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return View(paginatedMovies);
    }

    // GET: Movie/Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _movieService.GetMovieByIdAsync(id.Value);
        
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: Movie/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Genres = await _genreService.GetAllGenresAsync();
        return View();
    }

    // POST: Movie/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MovieVM model, IFormFile? photo)
    {
        try
        {
            string? imageFileName = null;
            
            // Upload photo if provided
            if (photo != null)
            {
                // Build file path and save
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", photo.FileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }

                imageFileName = photo.FileName;
            }
            
            // Map ViewModel to Model
            var movie = new Movie
            {
                Name = model.movie.Name,
                GenreId = model.movie.GenreId,
                DateAdded = DateTime.Now,
                ImageFile = imageFileName,
                Stock = 0,
                ReleaseDate = null
            };
            
            // Use service to save with audit
            await _movieService.AddMovieAsync(movie);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Log error and reload form
            ModelState.AddModelError("", $"Error creating movie: {ex.Message}");
            ViewBag.Genres = await _genreService.GetAllGenresAsync();
            return View(model);
        }
    }

    // GET: Movie/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _movieService.GetMovieByIdAsync(id.Value);
        if (movie == null)
        {
            return NotFound();
        }

        // Load genres for dropdown
        ViewBag.Genres = await _genreService.GetAllGenresAsync();
        return View(movie);
    }

    // POST: Movie/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GenreId")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _movieService.UpdateMovieAsync(movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // Collect ModelState errors
        ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        ViewBag.Genres = await _genreService.GetAllGenresAsync();
        return View(movie);
    }

    // GET: Movie/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _movieService.GetMovieByIdAsync(id.Value);
            
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: Movie/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _movieService.DeleteMovieAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> MovieExists(int id)
    {
        return await _movieService.MovieExistsAsync(id);
    }
}
