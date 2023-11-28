using BookOrca.Models;

namespace BookOrca.ApiAccess;

public interface IBookApi
{
    Task<BookInformation> GetBookInformation(string bookTitle);
}

