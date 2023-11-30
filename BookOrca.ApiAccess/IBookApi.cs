namespace BookOrca.ApiAccess;

public interface IBookApi
{
    Task<BookApiResult> GetBookInformation(string bookTitle);

    #region Singleton

    private static IBookApi? instance;

    public static IBookApi Instance
    {
        get => instance ??= new OpenLibraryService();
        set => instance = value;
    }

    #endregion
}