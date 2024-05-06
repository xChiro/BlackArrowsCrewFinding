namespace BKA.Tools.CrewFinding.Players.Exceptions;

public class PlayerNotFoundException(string id) : Exception($"Player with id {id} was not found");