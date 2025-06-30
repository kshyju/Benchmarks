using System.Text;
using System.Text.Json;

namespace Benchmarks.ConsoleApp;

public static class MetadataJsonHelper
{
    private static readonly JsonWriterOptions _relaxedJsonWriterOptions = new JsonWriterOptions
    {
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public static string SanitizePropertyValueInJson(string json, string propertyName)
    {
        // Avoid parsing if property not present in raw text
        if (json.IndexOf($"\"{propertyName}\"", StringComparison.OrdinalIgnoreCase) < 0)
        {
            return json;
        }

        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        bool matchFound = false;

        using var stream = new MemoryStream(json.Length);
        using var writer = new Utf8JsonWriter(stream, _relaxedJsonWriterOptions);

        writer.WriteStartObject();

        foreach (var property in root.EnumerateObject())
        {
            if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            {
                matchFound = true;
                var value = property.Value.GetString();
                writer.WriteString(property.Name, Sanitizer.Sanitize(value));
            }
            else
            {
                property.WriteTo(writer);
            }
        }

        writer.WriteEndObject();
        writer.Flush();

        return matchFound
            ? Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length)
            : json;
    }
}