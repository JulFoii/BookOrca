using NUnit.Framework;

namespace BookOrca.ApiAccess.Test;

[TestFixture]
public class OpenLibraryServiceTests
{
    private readonly OpenLibraryService openLibraryService = new();

    [Test]
    public async Task GetBookInformation_ValidTitle_ReturnsBookInfo()
    {
        // Arrange
        var validBookTitle = "Harry Potter"; // Gültiger Buchtitel

        // Act
        var bookResult = await openLibraryService.GetBookInformation(validBookTitle);

        Assert.That(bookResult.IsSuccessful);

        var bookInfo = bookResult.Book!;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(bookInfo.Title, Is.Not.Null);
            Assert.That(bookInfo.Author, Is.Not.Null);
            Assert.That(bookInfo.Isbn, Is.Not.Null);
            Assert.That(bookInfo.CoverUrl, Is.Not.Null);
        });
    }
}