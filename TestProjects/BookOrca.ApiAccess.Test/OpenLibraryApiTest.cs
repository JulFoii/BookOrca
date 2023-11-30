using NUnit.Framework;

namespace BookOrca.ApiAccess.Test;

[TestFixture]
public class OpenLibraryServiceTests
{
    private readonly OpenLibraryService openLibraryService = new OpenLibraryService();

    [Test]
    public async Task GetBookInformation_ValidTitle_ReturnsBookInfo()
    {
        // Arrange
        var validBookTitle = "Harry Potter"; // Gültiger Buchtitel

        // Act
        var bookInfo = await openLibraryService.GetBookInformation(validBookTitle);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(bookInfo, Is.Not.Null);
            Assert.That(bookInfo!.Title, Is.Not.Null);
            Assert.That(bookInfo.Author, Is.Not.Null);
            Assert.That(bookInfo.Isbn, Is.Not.Null);
            Assert.That(bookInfo.CoverUrl, Is.Not.Null);
        });
    }
}