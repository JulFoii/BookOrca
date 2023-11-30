namespace BookOrca.Resources;

public static class Paths
{
    public static string ImagesPath { get; } = Path.Combine("books", "images");
    public static string MetadataPath { get; } = Path.Combine("books", "metadata");
    public static string BookPath { get; } = Path.Combine("books");

    public static string GetImagePath(string fileName)
    {
        return Path.Combine(ImagesPath, $"{fileName}.png");
    }

    public static string GetAbsoluteImagePath(string fileName)
    {
        return RelativeToAbsolutePath(GetImagePath(fileName));
    }

    public static string GetMetadataPath(string fileName)
    {
        return Path.Combine(MetadataPath, $"{fileName}.json");
    }

    public static string GetAbsoluteMetadataPath(string fileName)
    {
        return RelativeToAbsolutePath(GetMetadataPath(fileName));
    }

    public static string GetBookPath(string fileName)
    {
        return Path.Combine(BookPath, fileName);
    }

    public static string GetAbsoluteBookPath(string fileName)
    {
        return RelativeToAbsolutePath(GetBookPath(fileName));
    }


    public static string RelativeToAbsolutePath(string relativePath)
    {
        return Path.GetFullPath(relativePath);
    }

    public static Uri GetAbsoluteUri(string path)
    {
        return Path.IsPathRooted(path)
            ? new Uri(path, UriKind.Absolute)
            : new Uri(RelativeToAbsolutePath(path), UriKind.Absolute);
    }
}