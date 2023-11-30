using BookOrca.Models;

namespace BookOrca.ApiAccess;

public interface IBookApi
{
    Task<Book> GetBookInformation(string bookTitle);
}