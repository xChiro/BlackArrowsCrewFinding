namespace BKA.Tools.CrewFinding.Crews.Exceptions;

public class CrewFullException : Exception
{
    public CrewFullException() : base($"Crew is full, cannot add more members.")
    {
    }
}