namespace BKA.Tools.CrewFinding.Crews.Commands.Expired;

public interface IExpiredCrewRemover
{
    public Task Remove(IExpiredCrewRemoverResponse response);
}