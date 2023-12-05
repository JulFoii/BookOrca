using System.Diagnostics;

namespace BookOrca.DataAccess;

public static class ConfigurationManager
{
    private const string Path = "config.txt";

    public static Dictionary<string, string> LoadConfiguration()
    {
        var lines = File.ReadAllLines(Path);

        var dict = new Dictionary<string, string>();
        
        foreach (var line in lines)
        {
            try
            {
                var values = line.Split('=', 2);
                dict.Add(values[0], values[1]);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        return dict;
    }

    public static void WriteConfiguration(Dictionary<string, string> dict)
    {
        var lines = new List<string>();

        foreach (var keyValuePair in dict)
        {
            lines.Add($"{keyValuePair.Key}={keyValuePair.Value}");
        }
        
        File.WriteAllLines(Path, lines);
    }
}