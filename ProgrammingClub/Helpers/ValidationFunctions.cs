namespace ProgrammingClub.Helpers
{
    public class ValidationFunctions
    {
        public static async Task TwoDatesValidator(DateTime? FirstDate, DateTime? SecondDate)
        {
            if(FirstDate!= null && SecondDate != null && FirstDate > SecondDate)
            {
                throw new IOException(Helpers.ErrorMessegesEnum.StartEndDatesError);
            }
        }

    }
}
