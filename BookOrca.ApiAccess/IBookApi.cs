using System.Threading.Tasks;
namespace BookOrca.ApiAccess;

public interface IBookApi
{
    Task<BookInformation> GetBookInformation(string bookTitle);
}

public class BookInformation
{
    public string Title { get; set; }
    public string[] Authors { get; set; }
    public string ISBN { get; set; }
    public string CoverUrl { get; set; }
}