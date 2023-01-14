using System.Text.Json;

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
