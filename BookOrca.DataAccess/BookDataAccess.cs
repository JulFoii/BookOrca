using BookOrca.DataAccess.Services;
using BookOrca.Models;
using BookOrca.Resources;

namespace BookOrca.DataAccess;

public class BookDataAccess : IBookDataAccess
{
    public void SaveBook(Book book)
    {
        JsonDataAccess.SaveObj(book, Paths.GetMetadataPath(Path.GetFileName(book.FileName)));
    }

    public void DeleteBook(Book book)
    {
        var fileName = Path.GetFileName(book.FileName);

        File.Delete(Paths.GetBookPath(fileName));
        File.Delete(Paths.GetImagePath(fileName));
        File.Delete(Paths.GetMetadataPath(fileName));
    }

    public IEnumerable<string> GetBookPaths()
    {
        return Directory.GetFiles(Paths.BookPath);
    }

    public Book LoadBook(string fileName)
    {
        return JsonDataAccess.LoadObj<Book>(Paths.GetMetadataPath(fileName));
    }

    public IEnumerable<Book> LoadBooks()
    {
        var files = Directory.GetFiles(Paths.MetadataPath);

        foreach (var file in files) yield return JsonDataAccess.LoadObj<Book>(file);
    }

    public async Task DownloadBookCover(Book book)
    {
        var coverPath = Paths.GetAbsoluteImagePath(book.FileName);

        await ImageDataAccess.DownloadImage(book.CoverUrl, coverPath);
    }
}