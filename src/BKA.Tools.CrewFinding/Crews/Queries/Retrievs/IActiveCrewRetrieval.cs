using System.Threading.Tasks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Queries.Recent;

public interface IActiveCrewRetrieval
{
    public Task Retrieve(string crewId, ICrewResponse response);
}