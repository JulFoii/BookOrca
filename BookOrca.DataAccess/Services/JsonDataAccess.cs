using Newtonsoft.Json;

namespace BookOrca.DataAccess.Services;

internal static class JsonDataAccess
{
    internal static void SaveObj(object obj, string path)
    {
        var jsonData = JsonConvert.SerializeObject(obj);

        File.WriteAllText(path, jsonData);
    }

    internal static T LoadObj<T>(string path)
    {
        return (T)JsonConvert.DeserializeObject(path)!;
    }
}