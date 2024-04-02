namespace BKA.Tools.CrewFinding.CrewParties.Exceptions;

public class CrewPartyFullException : Exception
{
    public CrewPartyFullException(string id) : base($"Crew party is full with {id}")
    {
    }
}