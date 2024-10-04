using Newtonsoft.Json;

namespace MachineLearning.Core.Utils;

public static class HttpExtensions
{
    public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage response)
    {
        var serializer = new JsonSerializer();

        using var stream = await response.Content.ReadAsStreamAsync();
        using var sr = new StreamReader(stream);
        using var jsonTextReader = new JsonTextReader(sr);

        return serializer.Deserialize<T>(jsonTextReader);
    }
}