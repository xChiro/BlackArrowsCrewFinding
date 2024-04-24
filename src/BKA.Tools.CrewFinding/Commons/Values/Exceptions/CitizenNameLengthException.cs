namespace BKA.Tools.CrewFinding.Commons.Values.Exceptions;

public class CitizenNameLengthException : Exception
{
    public CitizenNameLengthException(int minLength) : base(
        $"Star Citizen name must be at least {minLength} characters long")
    {
    }
}