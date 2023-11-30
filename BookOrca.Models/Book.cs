namespace BookOrca.Models;

public class Book
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string CoverPath { get; set; } = string.Empty;
   
    public string CoverUrl { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public string Isbn { get; set; } = string.Empty;
}