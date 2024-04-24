namespace BKA.Tools.CrewFinding.Commons.Values.Exceptions;

public class UserIdInvalidException : Exception
{
    public UserIdInvalidException(string message = "User Id is required") : base(message)
    {
    }
}