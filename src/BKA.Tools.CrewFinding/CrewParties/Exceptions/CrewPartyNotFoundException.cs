namespace BKA.Tools.CrewFinding.CrewParties.Exceptions;

public class CrewPartyNotFoundException : Exception
{
    public CrewPartyNotFoundException(string id) : base($"Crew party not found with {id}")
    { }
}