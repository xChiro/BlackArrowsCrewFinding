using BKA.Tools.CrewFinding.Commons.Exceptions;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Players.Exceptions;

namespace BKA.Tools.CrewFinding.Players;

public class Player : Entity
{
    public string CitizenName { get; }

    private Player(string id, string citizenName)
    {
        Id = id;
        CitizenName = citizenName;
    }

    public static Player Create(string id, string citizenName, int minLength = 3, int maxLength = 30)
    {
        (id, citizenName) = NormalizeInput(id, citizenName);
        ValidateInput(id, citizenName, minLength, maxLength);
        return new Player(id, citizenName);
    }

    private static (string, string) NormalizeInput(string id, string citizenName)
    {
        return (id.Trim(), citizenName.Trim());
    }

    private static void ValidateInput(string id, string citizenName, int minLength, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new UserIdInvalidException();

        if (string.IsNullOrWhiteSpace(citizenName))
            throw new CitizenNameEmptyException();

        if (citizenName.Length < minLength || citizenName.Length > maxLength)
            throw new CitizenNameLengthException(minLength);
    }
}