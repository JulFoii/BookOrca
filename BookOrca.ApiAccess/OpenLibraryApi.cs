using Newtonsoft.Json;

public class TestApi
{
    private static async Task GetBookInformationFromOpenLibrary(string bookTitle)
    {
        var apiUrl = $"https://openlibrary.org/search.json?title={Uri.EscapeDataString(bookTitle)}";

        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                // Parsing des JSON-Antwortkörpers
                dynamic data = JsonConvert.DeserializeObject(responseBody);

                // Prüfen, ob Daten vorhanden sind und Ausgabe der Informationen für das erste Buch
                if (data != null && data.docs != null && data.docs.Count > 0)
                {
                    var book = data.docs[0];
                    string title = book.title;
                    string[] authors = book.author_name.ToObject<string[]>();
                    string isbn = book.isbn != null && book.isbn.Count > 0 ? book.isbn[0] : "ISBN nicht verfügbar";
                    var coverId = (int)book.cover_i;
                    var coverUrl = $"https://covers.openlibrary.org/b/id/{coverId}-M.jpg";

                    Console.WriteLine("Titel: " + title);
                    Console.WriteLine("Autor(en): " + string.Join(", ", authors));
                    Console.WriteLine("ISBN: " + isbn);
                    Console.WriteLine("Cover URL: " + coverUrl);
                }
                else
                {
                    Console.WriteLine("Keine Informationen gefunden für " + bookTitle);
                }
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Fehler beim Abrufen der Buchinformationen: " + e.Message);
        }
    }
}