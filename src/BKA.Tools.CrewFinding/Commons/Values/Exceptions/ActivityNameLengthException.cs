namespace BKA.Tools.CrewFinding.Commons.Values.Exceptions;

public class ActivityNameLengthException(int maxLength)
    : Exception($"The activity name must be between 1 and {maxLength} characters.");