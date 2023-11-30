using BookOrca.Models;
using BookOrca.Resources;
using NUnit.Framework;

namespace BookOrca.DataAccess.Test;

public class BookDataAccessTest
{
    [SetUp]
    public void SetUp()
    {
        Directory.CreateDirectory(Paths.BookPath);
        Directory.CreateDirectory(Paths.ImagesPath);
        Directory.CreateDirectory(Paths.MetadataPath);
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            Directory.Delete(Paths.BookPath, true);
        }
        catch
        {
            // Ignore if not exists
        }
    }

    internal static Book CreateBook(string name = "test book")
    {
        return new Book
        {
            Autor = "Test 123",
            Isbn = "ISBN-123-123-123",
            FileName = $"{name}.pdf",
            Title = "The great tester",
            CoverUrl = "https://covers.openlibrary.org/b/id/13264887-M.jpg"
        };
    }

    [Test]
    public void TestSaveBook()
    {
        var bookDataAcces = new BookDataAccess();

        var book = CreateBook();

        bookDataAcces.SaveBook(book);

        Assert.That(File.Exists(Paths.GetMetadataPath("test book.pdf")));
    }

    [Test]
    public void TestLoadBook()
    {
        var bookDataAcces = new BookDataAccess();

        var book = CreateBook();

        bookDataAcces.SaveBook(book);

        var loadedBook = bookDataAcces.LoadBook(book.FileName);

        foreach (var property in book.GetType().GetProperties())
            Assert.That(property.GetValue(loadedBook), Is.EqualTo(property.GetValue(book)));
    }

    [Test]
    public async Task TestDeleteBook()
    {
        var bookDataAcces = new BookDataAccess();

        var book = CreateBook();

        bookDataAcces.SaveBook(book);

        await bookDataAcces.DownloadBookCover(book);

        var fileName = "test book.pdf";

        bookDataAcces.DeleteBook(book);
        Assert.Multiple(() =>
        {
            Assert.That(!File.Exists(Paths.GetBookPath(fileName)));
            Assert.That(!File.Exists(Paths.GetImagePath(fileName)));
            Assert.That(!File.Exists(Paths.GetMetadataPath(fileName)));
        });
    }

    [Test]
    public void TestGetBookPaths()
    {
        var bookDataAcces = new BookDataAccess();

        const string file1 = "book1.pdf";
        const string file2 = "book2.pdf";

        File.WriteAllText(Paths.GetBookPath(file1), "Test1");
        File.WriteAllText(Paths.GetBookPath(file2), "Test2");

        var bookPaths = bookDataAcces.GetBookPaths().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(bookPaths, Has.Count.EqualTo(2));
            Assert.That(Path.GetFullPath(bookPaths[0]),
                Is.EqualTo(Paths.GetAbsoluteBookPath(file1)));
            Assert.That(Path.GetFullPath(bookPaths[1]),
                Is.EqualTo(Paths.GetAbsoluteBookPath(file2)));
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