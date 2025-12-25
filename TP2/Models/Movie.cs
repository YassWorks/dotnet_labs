namespace TP2.Models;

public class Movie
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public Guid GenreId { get; set; }

    public Genre? Genre { get; set; }

    // Optional image file name
    public string? ImageFile { get; set; }
    
    // Date added
    public DateTime? DateAdded { get; set; }
    
    // Stock quantity
    public int Stock { get; set; }
    
    // Release date
    public DateTime? ReleaseDate { get; set; }

    public Movie() {}
}
