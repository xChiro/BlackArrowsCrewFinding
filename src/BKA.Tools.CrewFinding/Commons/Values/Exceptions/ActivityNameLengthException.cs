namespace BKA.Tools.CrewFinding.Commons.Values.Exceptions;

public class ActivityNameLengthException : Exception
{
    public ActivityNameLengthException(int maxLength) : base(
        $"The activity name must be between 1 and {maxLength} characters.")
    {
    }
}