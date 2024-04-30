namespace BKA.Tools.CrewFinding.Commons.Values.Exceptions;

public class ActivityDescriptionLengthException(int maxDescriptionLength)
    : Exception($"The description length is too long. The maximum length is {maxDescriptionLength}.");