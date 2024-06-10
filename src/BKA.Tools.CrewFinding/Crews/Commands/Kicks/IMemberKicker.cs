namespace BKA.Tools.CrewFinding.Crews.Commands.Kicks;

public interface IMemberKicker
{
    public Task Kick(string memberId);
}