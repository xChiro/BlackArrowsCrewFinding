namespace BKA.Tools.CrewFinding.Crews.Exceptions;

public class CrewNotFoundException(string id) : Exception($"Crew not found with {id}")
{
}