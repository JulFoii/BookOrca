using BookOrca.Models;

namespace BookOrca.DataAccess;

public interface IBookDataAccess
{
    public void SaveBook(Book book);

    public Book LoadBook(string fileName);

    public void DeleteBook(string fileName);
    
    public IEnumerable<string> GetBookPaths();

    public IEnumerable<Book> LoadBooks();

    public Task DownloadBookCover(Book book);
}