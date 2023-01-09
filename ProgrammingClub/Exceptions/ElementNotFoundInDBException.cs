namespace ProgrammingClub.Exceptions
{
    public class ElementNotFoundInDBException : DatabaseEntryException
    {
        public ElementNotFoundInDBException() { }

        public ElementNotFoundInDBException(string message) : base(message) { }
    }
}
