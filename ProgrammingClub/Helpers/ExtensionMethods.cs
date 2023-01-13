using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Helpers
{
    public static class ExtensionMethods
    {
        public static string GetLoggingInfo(this object obj)
        {
           string jsonString = JsonSerializer.Serialize(obj);
           return jsonString;
        }

    }
}
