namespace ProgrammingClub.Helpers
{
    public static class ErrorMessegesEnum
    {
        public const string NoElementFound = "No element found";
        public const string NoMemberFound = "Member doesn't exist";
        public const string StartEndDatesError = "End date cannot be smaller than start date";
        public const string ZeroUpdatesToSave = "There are no updates to save";

        public static class CodeSnippet
        {
            public const string NoCodeSnippetFound = "Code Snippet does't exist";
            public const string idCSPreviousIdenticalToIdCS = "IdCodeSnippetPreviousVersion cannot be equal to IdCodeSnippet";
        }
    }
}
