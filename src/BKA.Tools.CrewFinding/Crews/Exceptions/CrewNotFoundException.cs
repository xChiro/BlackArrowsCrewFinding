namespace BKA.Tools.CrewFinding.Crews.Exceptions;

public class CrewNotFoundException : Exception
{
    public CrewNotFoundException(string id) : base($"Crew not found with {id}")
    { }
}