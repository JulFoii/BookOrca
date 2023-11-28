using System;
using System.Net.Http;
using System.Threading.Tasks;

class TestApi
{
    static async Task GetBookInformationFromOpenLibrary(string bookTitle)
    {
        string apiUrl = $"https://openlibrary.org/search.json?title={Uri.EscapeDataString(bookTitle)}";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // Parsing des JSON-Antwortkörpers
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

                // Prüfen, ob Daten vorhanden sind und Ausgabe der Informationen für das erste Buch
                if (data != null && data.docs != null && data.docs.Count > 0)
                {
                    dynamic book = data.docs[0];
                    string title = book.title;
                    string[] authors = book.author_name.ToObject<string[]>();
                    string isbn = (book.isbn != null && book.isbn.Count > 0) ? book.isbn[0] : "ISBN nicht verfügbar";
                    int coverId = (int)book.cover_i;
                    string coverUrl = $"https://covers.openlibrary.org/b/id/{coverId}-M.jpg";

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