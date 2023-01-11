namespace ProgrammingClub.Helpers
{
    public static class ErrorMessagesEnum
    {
        public const string NoElementFound = "No element found in table!";
        public const string IncorrectSize = "Incorrect size";
        public const string InternalServerError = "Internal server error please contact your administrator";
        public const string CantDeleteEvent = "The event can't be deleted";
        public const string ZeroUpdatesToSave = "There are no updates to save";

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
