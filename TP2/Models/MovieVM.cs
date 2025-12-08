namespace TP2.Models;

/// <summary>
/// ViewModel for creating/editing a movie (optional image upload)
/// </summary>
public class MovieVM
{
    // Movie data
    public Movie movie { get; set; } = new Movie { Name = string.Empty };
    
    // Optional uploaded image
    public IFormFile? photo { get; set; }
}
