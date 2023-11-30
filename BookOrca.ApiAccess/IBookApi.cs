using BookOrca.Models;

namespace BookOrca.ApiAccess;

public interface IBookApi
{
    Task<Book?> GetBookInformation(string bookTitle);

    #region Singleton

    private static IBookApi? instance;

    public static IBookApi Instance
    {
        get => instance ??= new OpenLibraryService();
        set => instance = value;
    }

    #endregion
}