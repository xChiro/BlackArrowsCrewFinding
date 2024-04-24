namespace BKA.Tools.CrewFinding.Commons.Values.Exceptions;

public class CitizenNameEmptyException : Exception
{
    public CitizenNameEmptyException(string message = "Star Citizen name is required") : base(message)
    {
    }
}