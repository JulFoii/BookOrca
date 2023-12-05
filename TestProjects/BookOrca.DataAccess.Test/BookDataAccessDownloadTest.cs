using BookOrca.Resources;
using NUnit.Framework;

namespace BookOrca.DataAccess.Test;

public class BookDataAccessDownloadTest
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

    [Test]
    public async Task TestDownloadImage()
    {
        var bookDataAcces = new BookDataAccess();

        var book = BookDataAccessTest.CreateBook();

        await bookDataAcces.DownloadBookCover(book);

        Assert.That(File.Exists(Paths.GetImagePath(book.FileName)));
    }
}