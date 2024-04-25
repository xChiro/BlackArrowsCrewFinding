namespace BKA.Tools.CrewFinding.Crews.Exceptions;

public class CrewDisbandException : Exception
{
    public CrewDisbandException() : base(
        "Crew disbandment failed, due user is not the owner of the crew or crew does not exist.")
    {
    }
}