using System.Text.Json;

namespace WebFront.Core.Helper
{
    internal static class ExtensionHelper
    {
        public static T? ConvertWithJson<T>(this object? data)
        {
            return data == null ? default : JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(data));
        }
    }
}
