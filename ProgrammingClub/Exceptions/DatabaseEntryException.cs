namespace ProgrammingClub.Exceptions
{
    [Serializable]
    public class DatabaseEntryException : Exception
    {
        public DatabaseEntryException() { }

        public DatabaseEntryException(string message) : base(message) { }
    }
}
