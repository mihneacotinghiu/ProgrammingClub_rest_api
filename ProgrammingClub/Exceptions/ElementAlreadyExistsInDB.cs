namespace ProgrammingClub.Exceptions
{
    public class ElementAlreadyExistsInDB : DatabaseEntryException
    {
        public ElementAlreadyExistsInDB() { }
        public ElementAlreadyExistsInDB(string message) : base(message) { }
    }
}
