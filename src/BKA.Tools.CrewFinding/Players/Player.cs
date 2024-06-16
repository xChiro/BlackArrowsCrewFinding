using BKA.Tools.CrewFinding.Commons;
using BKA.Tools.CrewFinding.Commons.Exceptions;

namespace BKA.Tools.CrewFinding.Players;

public class Player : Entity
{
    public HandlerName CitizenName { get; private set; }

    private Player(string id, HandlerName citizenName)
    {
        Id = id;
        CitizenName = citizenName;
    }

    public static Player Create(string id, string citizenName, int minLength, int maxLength)
    {
        ValidateInput(id);
        var handlerName = HandlerName.Create(citizenName, minLength, maxLength);
        
        return new Player(id, handlerName);
    }

    private static void ValidateInput(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new UserIdInvalidException();
    }

    public void UpdateName(string newName, int minLength, int maxLength)
    {
        CitizenName = HandlerName.Create(newName, minLength, maxLength);
    }
}