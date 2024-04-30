namespace BKA.Tools.CrewFinding.Crews.Exceptions;

public class PlayerMultipleCrewsException(string message = "Player is already in a crew") : Exception(message);