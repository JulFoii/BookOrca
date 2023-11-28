using BookOrca.Models;
using NUnit.Framework;

namespace BookOrca.DataAccess.Test;

public class BookDataAccessTest
{
    [SetUp]
    public void SetUp()
    {
        Directory.CreateDirectory("books");
        Directory.CreateDirectory("books/metadata");
        Directory.CreateDirectory("books/metadata/images");
        Directory.CreateDirectory("books/metadata/data");
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            Directory.Delete("books", true);
        }
        catch
        {
            // Ignore if not exists
        }
    }

    private Book CreateBook(string name = "test book")
    {
        return new Book
        {
            Autor = "Test 123",
            Isbn = "ISBN-123-123-123",
            Path = $"books/{name}.pdf",
            Titel = "The great tester",
            CoverUrl = "https://covers.openlibrary.org/b/id/13264887-M.jpg"
        };
    }

    [Test]
    public void TestSaveBook()
    {
        var bookDataAcces = new BookDataAccess();

        var book = CreateBook();

        bookDataAcces.SaveBook(book);

        Assert.That(File.Exists("books/metadata/data/test book.pdf.json"));
    }

    [Test]
    public void TestLoadBook()
    {
        var bookDataAcces = new BookDataAccess();

        var book = CreateBook();

        bookDataAcces.SaveBook(book);

        var loadedBook = bookDataAcces.LoadBook(book.Path);

        foreach (var property in book.GetType().GetProperties())
            Assert.That(property.GetValue(loadedBook), Is.EqualTo(property.GetValue(book)));
    }

    [Test]
    public async Task TestDownloadImage()
    {
        var bookDataAcces = new BookDataAccess();

        var book = CreateBook();

        await bookDataAcces.DownloadBookCover(book);

        Assert.That(File.Exists(book.CoverPath));
    }

    [Test]
    public async Task TestDeleteBook()
    {
        var bookDataAcces = new BookDataAccess();

        var book = CreateBook();

        bookDataAcces.SaveBook(book);

        await bookDataAcces.DownloadBookCover(book);

        bookDataAcces.DeleteBook(book);
        Assert.Multiple(() =>
        {
            Assert.That(!File.Exists("books/test book.pdf"));
            Assert.That(!File.Exists("books/metadata/images/test book.pdf.png"));
            Assert.That(!File.Exists("books/metadata/data/test book.pdf.json"));
        });
    }

    [Test]
    public void TestGetBookPaths()
    {
        var bookDataAcces = new BookDataAccess();

        const string path1 = "books/book1.pdf";
        const string path2 = "books/book2.pdf";

        File.WriteAllText(path1, "Test1");
        File.WriteAllText(path2, "Test2");

        var bookPaths = bookDataAcces.GetBookPaths().ToList();
        
        Assert.Multiple(() =>
        {
            Assert.That(bookPaths, Has.Count.EqualTo(2));
            Assert.That(Path.GetFullPath(bookPaths[0]), Is.EqualTo(Path.GetFullPath(path1)));
            Assert.That(Path.GetFullPath(bookPaths[1]), Is.EqualTo(Path.GetFullPath(path2)));
        });
    }

    [Test]
    public void TestLoadBooks()
    {
        var bookDataAccess = new BookDataAccess();

        var book1 = CreateBook();
        var book2 = CreateBook("Test book 2");
        
        bookDataAccess.SaveBook(book1);
        bookDataAccess.SaveBook(book2);

        var loadedBooks = bookDataAccess.LoadBooks().ToList();
        
        Assert.That(loadedBooks, Has.Count.EqualTo(2));
    }
}