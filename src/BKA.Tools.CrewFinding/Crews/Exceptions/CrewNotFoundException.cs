namespace BKA.Tools.CrewFinding.Crews.Exceptions;

public class CrewNotFoundException(string? id = null) : Exception(id != null ? $"Crew not found with {id}" : "Crew not found")
{
}