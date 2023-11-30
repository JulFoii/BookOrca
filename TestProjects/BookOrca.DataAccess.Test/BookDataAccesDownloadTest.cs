using BookOrca.Resources;
using NUnit.Framework;

namespace BookOrca.DataAccess.Test;

public class BookDataAccesDownloadTest
{
    [Test]
    public async Task TestDownloadImage()
    {
        var bookDataAcces = new BookDataAccess();

        var book = BookDataAccessTest.CreateBook();

        await bookDataAcces.DownloadBookCover(book);

        Assert.That(File.Exists(Paths.GetImagePath(book.FileName)));
    }
}