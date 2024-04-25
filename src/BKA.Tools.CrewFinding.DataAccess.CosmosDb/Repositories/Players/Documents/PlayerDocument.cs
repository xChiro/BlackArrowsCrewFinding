using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players.Documents;

public class PlayerDocument
{
    public string Id { get; set; }

    public string CitizenName { get; set; }

    public static PlayerDocument CreateFromPlayer(Player player)
    {
        return new PlayerDocument
        {
            Id = player.Id,
            CitizenName = player.CitizenName
        };
    }

    public Player ToPlayer()
    {
        return Player.Create(Id, CitizenName);
    }
}