namespace TP2.Models;

public class MovieVM
{
    // Movie data
    public Movie movie { get; set; } = new Movie { Name = string.Empty };
    
    // Optional uploaded image
    public IFormFile? photo { get; set; }
}
