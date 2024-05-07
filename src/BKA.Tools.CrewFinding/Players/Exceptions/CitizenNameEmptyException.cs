namespace BKA.Tools.CrewFinding.Players.Exceptions;

public class CitizenNameEmptyException(string message = "Star Citizen name is required") : Exception(message);