using System.Net;

namespace BookOrca.DataAccess.Services;

internal static class ImageDataAccess
{
    internal static async Task DownloadImage(string url, string path)
    {
        using var httpClient = new HttpClient();

        var bytes = await httpClient.GetByteArrayAsync(url);
        
        await File.WriteAllBytesAsync(path, bytes);
    } 
}