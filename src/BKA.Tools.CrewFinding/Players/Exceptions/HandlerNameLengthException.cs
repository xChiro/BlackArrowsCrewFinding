namespace BKA.Tools.CrewFinding.Players.Exceptions;

public class HandlerNameLengthException(int minLength)
    : Exception($"Star Citizen name must be at least {minLength} characters long");