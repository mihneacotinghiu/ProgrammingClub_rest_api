namespace ProgrammingClub.Helpers
{
    public static class ErrorMessagesEnum
    {
        public const string NoElementFound = "No element found in table!";
        public const string IncorectSize= "Incorext size";
        public const string StartEndDatesError = "End date cannot be smaller than start date";
        public const string ZeroUpdatesToSave = "There are no updates to save";

        public static class CodeSnippet
        {
            public const string NoCodeSnippetFound = "Code Snippet does't exist";
            public const string idCSPreviousIdenticalToIdCS = "IdCodeSnippetPreviousVersion cannot be equal to IdCodeSnippet";
        }
        public static class Member
        {
            public const string NoMemberFound = "Member doesn't exist";
        }

    }
}
