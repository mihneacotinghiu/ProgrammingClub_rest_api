using ProgrammingClub.Exceptions;

namespace ProgrammingClub.Helpers
{
    public class ValidationFunctions
    {
        public static void TrowExceptionWhenDateIsNotValid(DateTime? startDate, DateTime? endDate)
        {
            if(startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                throw new ModelValidationException(Helpers.ErrorMessagesEnum.StartEndDatesError);
            }
        }

    }
}
