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
        public const string EmptyField = "Please complete all fields!";
        public const string Event_ID_NotFound = "Event Id not found!";
        public const string ID_NotFound = "Id not found!";
        public const string Dropout_ID_NotFound = "Dropout  Id not found!";
        public const string Moderator_ID_NotFound = "Moderator  Id not found!";
        public const string Member_ID_NotFound = "Member  Id not found!";

        public static class CodeSnippet
        {
            public const string NotFound = "Code Snippet does't exist";
            public const string IdCSPreviousIdenticalToIdCS = "IdCodeSnippetPreviousVersion cannot be equal to IdCodeSnippet";
        }
        public static class Member
        {
            public const string NoMemberFound = "Member doesn't exist";
        }

        public static class EventsParticipantMessage
        {
            public const string ElementAlreadyExists = "Already in the participant list for the given event";
            public const string MemberDoesNotExist = "Member does not exist";
            public const string EventDoesNotExist = "Event does not exist";
        }
        public static class Moderator
        {
            public const string NoModeratorFound = "This Moderator does not exist";
        }

        public const string AlreadyExistsById = "Already Exists By Id";
        public const string NoUpdates = "No updates!";
        public static class EventType
        {
            public const string NoEventTypeFound = "This EventType does not exist";
        }

    }
}
