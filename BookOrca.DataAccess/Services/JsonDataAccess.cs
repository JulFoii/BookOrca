using Newtonsoft.Json;

namespace BookOrca.DataAccess.Services;

internal static class JsonDataAccess
{
    internal static void SaveObj(object obj, string path)
    {
        var jsonData = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        });

        File.WriteAllText(path, jsonData);
    }

    internal static T LoadObj<T>(string path)
    {
        var jsonData = File.ReadAllText(path);
        
        var data = JsonConvert.DeserializeObject(jsonData);

        return (T)data!;
    }
}