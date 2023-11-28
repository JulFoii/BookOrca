using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            BookInformation bookInfo = await _openLibraryService.GetBookInformation(validBookTitle);

            // Assert
            Console.WriteLine(bookInfo.Title + bookInfo.Authors + bookInfo.ISBN + bookInfo.CoverUrl);
            Assert.IsNotNull(bookInfo);
            Assert.IsNotNull(bookInfo.Title);
            Assert.IsNotNull(bookInfo.Authors[0]);
            Assert.IsNotNull(bookInfo.ISBN);
            Assert.IsNotNull(bookInfo.CoverUrl);
        }
    }
}
