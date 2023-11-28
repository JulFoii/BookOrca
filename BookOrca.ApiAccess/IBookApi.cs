using BookOrca.Models;

namespace BookOrca.ApiAccess;

public interface IBookApi
{
    Task<BookInformation> GetBookInformation(string bookTitle);
}

public class BookInformation
{
    public string Title { get; set; } = string.Empty;
    public string[] Authors { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string CoverUrl { get; set; } = string.Empty;
}

