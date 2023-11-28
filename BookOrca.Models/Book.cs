namespace BookOrca.Models;

public class Book
{
    public string Titel { get; set; } = string.Empty;

    public string Autor { get; set; } = string.Empty;

    public string? CoverPath { get; set; }

    public string CoverUrl { get; set; } = string.Empty;
    
    public string Path { get; set; } = string.Empty;

    public string Isbn { get; set; } = string.Empty;
    
    public Book()
    {
        
    }
}