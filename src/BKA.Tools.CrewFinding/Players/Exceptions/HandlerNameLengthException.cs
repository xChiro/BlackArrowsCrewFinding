namespace BKA.Tools.CrewFinding.Players.Exceptions;

public class HandlerNameLengthException(int minLength, int maxLength)
    : Exception($"Star Citizen name must be between {minLength} and {maxLength} characters long");