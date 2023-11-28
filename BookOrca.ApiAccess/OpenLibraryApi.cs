﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class OpenLibraryService : IBookApi
{
    public async Task<BookInformation> GetBookInformation(string bookTitle)
    {
        string apiUrl = $"https://openlibrary.org/search.json?title={Uri.EscapeDataString(bookTitle)}";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(responseBody);

                if (data != null && data.docs != null && data.docs.Count > 0)
                {
                    dynamic book = data.docs[0];
                    string title = book.title;
                    string[] authors = book.author_name.ToObject<string[]>();
                    string isbn = (book.isbn != null && book.isbn.Count > 0) ? book.isbn[0] : "ISBN nicht verfügbar";
                    int coverId = (int)book.cover_i;
                    string coverUrl = $"https://covers.openlibrary.org/b/id/{coverId}-M.jpg";

                    return new BookInformation
                    {
                        Title = title,
                        Authors = authors,
                        ISBN = isbn,
                        CoverUrl = coverUrl
                    };
                }
                else
                {
                    // Keine Informationen gefunden
                    return null;
                }
            }
        }
        catch (HttpRequestException e)
        {
            // Fehler beim Abrufen der Daten
            Console.WriteLine("Fehler beim Abrufen der Buchinformationen: " + e.Message);
            return null;
        }
    }
}