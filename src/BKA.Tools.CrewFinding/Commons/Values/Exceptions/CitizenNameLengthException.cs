namespace BKA.Tools.CrewFinding.Commons.Values.Exceptions;

public class CitizenNameLengthException(int minLength)
    : Exception($"Star Citizen name must be at least {minLength} characters long");