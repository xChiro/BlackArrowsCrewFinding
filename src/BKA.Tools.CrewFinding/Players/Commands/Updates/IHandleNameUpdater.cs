namespace BKA.Tools.CrewFinding.Players.Commands.Updates;

public interface IHandleNameUpdater
{
    public Task Update(string newName);
}