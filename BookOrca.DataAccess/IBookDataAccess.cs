using BookOrca.Models;

namespace BookOrca.DataAccess;

public interface IBookDataAccess
{
    private static IBookDataAccess? instance;
    public static IBookDataAccess Instance
    {
        get => instance ??= new BookDataAccess();
        set => instance = new BookDataAccess();
    }

    public void SaveBook(Book book);

    public Book LoadBook(string fileName);

    public void DeleteBook(Book book);
    
    public IEnumerable<string> GetBookPaths();

    public IEnumerable<Book> LoadBooks();

    public Task DownloadBookCover(Book book);
}