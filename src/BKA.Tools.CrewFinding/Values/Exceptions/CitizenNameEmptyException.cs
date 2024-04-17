namespace BKA.Tools.CrewFinding.Values.Exceptions;

public class CitizenNameEmptyException : Exception
{
    public CitizenNameEmptyException(string message = "Star Citizen name is required") : base(message)
    {
    }
}