using BookOrca.DataAccess.Services;
using BookOrca.Models;

namespace BookOrca.DataAccess;

public class BookDataAccess : IBookDataAccess
{
    private const string BooksPath = "books";
    private const string DataPath = "books/metadata/data";
    private const string ImagePath = "books/metadata/images";

    public void SaveBook(Book book)
    {
        JsonDataAccess.SaveObj(book, $"{DataPath}/{Path.GetFileName(book.Path)}.json");
    }

    public IEnumerable<string> GetBookPaths()
    {
        return Directory.GetFiles(BooksPath);
    }

    public Book LoadBook(string fileName)
    {
        return JsonDataAccess.LoadObj<Book>($"{DataPath}/{Path.GetFileName(fileName)}.json");
    }

    public IEnumerable<Book> LoadBooks()
    {
        var files = Directory.GetFiles(DataPath);

        foreach (var file in files)
        {
            yield return JsonDataAccess.LoadObj<Book>(file);
        }
    }

    public async Task DownloadBookCover(Book book)
    {
        book.CoverPath = $"{ImagePath}/{Path.GetFileName(book.Path)}.png";
        
        await ImageDataAccess.DownloadImage(book.CoverUrl, book.CoverPath);
    }
}