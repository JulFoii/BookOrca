using BookOrca.Models;
using Newtonsoft.Json;

namespace BookOrca.ApiAccess;

public class OpenLibraryService : IBookApi
{
    public async Task<BookApiResult> GetBookInformation(string bookTitle)
    {
        var apiUrl = $"https://openlibrary.org/search.json?title={Uri.EscapeDataString(bookTitle)}";

        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(responseBody)!;

            if (data != null && data!.docs != null && data!.docs.Count > 0)
            {
                var book = data!.docs[0];
                string title = book.title;
                string[] authors = book.author_name.ToObject<string[]>();
                string isbn = book.isbn != null && book.isbn.Count > 0 ? book.isbn[0] : "ISBN nicht verfügbar";
                var coverId = (int?)book.cover_i;

                var coverUrl = string.Empty;

                if (coverId != null) coverUrl = $"https://covers.openlibrary.org/b/id/{coverId}-M.jpg";

                return new BookApiResult(new Book
                {
                    Title = title,
                    Author = authors[0],
                    Isbn = isbn,
                    CoverUrl = coverUrl
                });
            }

            return new BookApiResult(responseBody);
        }
        catch (HttpRequestException e)
        {
            return new BookApiResult(e.Message);
        }
    }
}