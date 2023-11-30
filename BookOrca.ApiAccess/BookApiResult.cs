using BookOrca.Models;

namespace BookOrca.ApiAccess;

public record BookApiResult
{
    public BookApiResult(Book book)
    {
        Book = book;
        IsSuccessful = true;
    }

    public BookApiResult(string errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccessful = false;
    }

    public bool IsSuccessful { get; }
    public Book? Book { get; }
    public string? ErrorMessage { get; }
}