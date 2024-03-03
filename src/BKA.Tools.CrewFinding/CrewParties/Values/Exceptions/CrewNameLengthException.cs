namespace BKA.Tools.CrewFinding.CrewParties.Values.Exceptions;

public class CrewNameLengthException : Exception
{
    public CrewNameLengthException(string name, int maxLength) : base($"{name} contains too many characters, maximum is {maxLength}.")
    {
    }
}