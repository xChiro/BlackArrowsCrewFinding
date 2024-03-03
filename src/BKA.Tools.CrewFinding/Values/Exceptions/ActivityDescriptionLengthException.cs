namespace BKA.Tools.CrewFinding.Values.Exceptions;

public class ActivityDescriptionLengthException : Exception
{
    public ActivityDescriptionLengthException(int maxDescriptionLength)
        : base($"The description length is too long. The maximum length is {maxDescriptionLength}.")
    {
    }
}