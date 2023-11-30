using BookOrca.Models;
using Newtonsoft.Json;

namespace BookOrca.ApiAccess;

public class OpenLibraryService : IBookApi
{
    public async Task<Book> GetBookInformation(string bookTitle)
    {
        string apiUrl = $"https://openlibrary.org/search.json?title={Uri.EscapeDataString(bookTitle)}";

        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(responseBody)!;

            if (data != null && data!.docs != null && data!.docs.Count > 0)
            {
                dynamic book = data!.docs[0];
                string title = book.title;
                string[] authors = book.author_name.ToObject<string[]>();
                string isbn = (book.isbn != null && book.isbn.Count > 0) ? book.isbn[0] : "ISBN nicht verfügbar";
                int coverId = (int)book.cover_i;
                string coverUrl = $"https://covers.openlibrary.org/b/id/{coverId}-M.jpg";

                return new Book
                {
                    Title = title,
                    Author = authors[0],
                    Isbn = isbn,
                    CoverUrl = coverUrl
                };
            }
            
            return null!;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Fehler beim Abrufen der Buchinformationen: " + e.Message);
            
            return null!;
        }
    }
}
