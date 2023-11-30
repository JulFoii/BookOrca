using BookOrca.Models;
using NUnit.Framework;

namespace BookOrca.ApiAccess.Test
{
    [TestFixture]
    public class OpenLibraryServiceTests
    {
        public OpenLibraryService _openLibraryService;

        [SetUp]
        public void Setup()
        {
            // Initialisierung des Service
            _openLibraryService = new OpenLibraryService();
        }

        [Test]
        public async Task GetBookInformation_ValidTitle_ReturnsBookInfo()
        {
            // Arrange
            string validBookTitle = "Harry Potter"; // Gültiger Buchtitel

            // Act
            Book bookInfo = await _openLibraryService.GetBookInformation(validBookTitle);

            // Assert
            Console.WriteLine(bookInfo.Title + bookInfo.Author + bookInfo.Isbn + bookInfo.CoverUrl);
            Assert.IsNotNull(bookInfo);
            Assert.IsNotNull(bookInfo.Title);
            Assert.IsNotNull(bookInfo.Author[0]);
            Assert.IsNotNull(bookInfo.Isbn);
            Assert.IsNotNull(bookInfo.CoverUrl);
        }
    }
}
