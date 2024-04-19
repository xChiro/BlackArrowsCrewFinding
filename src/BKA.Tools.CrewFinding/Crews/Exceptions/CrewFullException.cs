namespace BKA.Tools.CrewFinding.Crews.Exceptions;

public class CrewFullException : Exception
{
    public CrewFullException(string id) : base($"Crew party is full with {id}")
    {
    }
}