namespace BKA.Tools.CrewFinding.Commons.Values.Exceptions;

public class ActivityDescriptionLengthException : Exception
{
    public ActivityDescriptionLengthException(int maxDescriptionLength)
        : base($"The description length is too long. The maximum length is {maxDescriptionLength}.")
    {
    }
}