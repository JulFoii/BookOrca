namespace BookOrca.ApiAccess;

public interface IBookApi
{
    Task<BookApiResult> GetBookInformation(string bookTitle);
}