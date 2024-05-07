namespace BKA.Tools.CrewFinding.Crews.Exceptions;

public class ActivityNameLengthException(int maxLength)
    : Exception($"The activity name must be between 1 and {maxLength} characters.");