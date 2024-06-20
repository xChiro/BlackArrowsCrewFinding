namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface IDomainLogger
{
    public void Log(Exception e, string message);
}