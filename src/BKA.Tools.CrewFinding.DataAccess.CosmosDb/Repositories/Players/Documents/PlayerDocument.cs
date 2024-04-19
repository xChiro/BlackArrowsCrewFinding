using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players.Documents;

public class PlayerDocument
{
    public PlayerDocument(string id, string citizenName)
    {
        Id = id;
        CitizenName = citizenName;
    }

    public string Id { get; set; }

    public string CitizenName { get; set; }

    public static PlayerDocument CreateFromPlayer(Player player)
    {
        return new PlayerDocument(player.Id, player.CitizenName);
    }
}