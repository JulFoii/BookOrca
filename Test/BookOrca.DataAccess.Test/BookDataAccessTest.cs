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
    
    [Test]
    public void TestSaveBook()
    {
        var bookDataAcces = new BookDataAccess();

        var book = new Book()
        {
            Autor = "Test 123",
            Isbn = "ISBN-123-123-123",
            Path = "books/test book.pdf",
            Titel = "The great tester",
            CoverUrl = "https://covers.openlibrary.org/b/id/13264887-M.jpg"
        };
        
        bookDataAcces.SaveBook(book);
        
        Assert.That(File.Exists("books/metadata/test book.pdf.json"));
    }
}