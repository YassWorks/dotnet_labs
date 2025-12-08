using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2.Models;
using TP2.Services.ServiceContracts;

namespace TP2.Controllers;

public class GenreController : Controller
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }
    // GET: Genre/
    public async Task<IActionResult> Index()
    {
        var genres = await _genreService.GetAllGenresAsync();
        return View(genres);
    }

    // GET: Genre/Details
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await _genreService.GetGenreByIdAsync(id.Value);
        
        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    // GET: Genre/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Genre/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Genre genre)
    {
        if (!ModelState.IsValid) return View(genre);
        genre.Id = Guid.NewGuid();
        await _genreService.AddGenreAsync(genre);
        return RedirectToAction(nameof(Index));
    }

    // GET: Genre/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await _genreService.GetGenreByIdAsync(id.Value);
        if (genre == null)
        {
            return NotFound();
        }
        return View(genre);
    }

    // POST: Genre/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Genre genre)
    {
        if (id != genre.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _genreService.UpdateGenreAsync(genre);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GenreExists(genre.Id))
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
        return View(genre);
    }

    // GET: Genre/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await _genreService.GetGenreByIdAsync(id.Value);
        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    // POST: Genre/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);
        if (genre != null)
        {
            // Check if there are movies associated with this genre
            if (await _genreService.HasMoviesAsync(id))
            {
                ModelState.AddModelError("", "Cannot delete genre because it is associated with one or more movies.");
                return View(genre);
            }
            await _genreService.DeleteGenreAsync(id);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> GenreExists(Guid id)
    {
        return await _genreService.GenreExistsAsync(id);
    }
}
