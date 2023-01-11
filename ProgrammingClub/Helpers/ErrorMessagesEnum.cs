namespace ProgrammingClub.Helpers
{
    public static class ErrorMessagesEnum
    {
        public const string NoElementFound = "No element found in table!";
        public const string IncorrectSize = "Incorrect size";
        public const string InternalServerError = "Internal server error please contact your administrator";
        public const string CantDeleteEvent = "The event can't be deleted";
        public const string ZeroUpdatesToSave = "There are no updates to save";
        public const string StartEndDatesError = "End date cannot be smaller than start date";

        public static class CodeSnippet
        {
            public const string NotFound = "Code Snippet does't exist";
            public const string IdCSPreviousIdenticalToIdCS = "IdCodeSnippetPreviousVersion cannot be equal to IdCodeSnippet";
        }
        public static class Member
        {
            public const string NoMemberFound = "Member doesn't exist";
        }
        public static class Moderator
        {
            public const string NoModeratorFound = "Moderator does not exist";
        }

        public static class EventType
        {
            public const string NoEventTypeFound = "EventType does not exist";
        }

    }
}
