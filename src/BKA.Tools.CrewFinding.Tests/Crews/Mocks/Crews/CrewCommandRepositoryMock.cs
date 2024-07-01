using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Tests.Commons;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

public class CrewCommandRepositoryMock(Crew? crew = null) : ICrewCommandRepository, ICrewDisbandRepository
{
    public int CreateCrewCalLCount { get; private set; }

    public Crew Crew { get; private set; } =
        crew ?? CrewBuilder.Build("312312", Player.Create("1231234", "Captain", 2, 18));

    public string[] DisbandedCrewIds { get; private set; } = [];
    public string DeletedPlayerId { get; set; } = string.Empty;
    public string DeletedCrewId { get; private set; } = string.Empty;

    public Task CreateCrew(Crew crew)
    {
        CreateCrewCalLCount++;
        return Task.FromResult(Crew = crew);
    }

    public Task UpdateMembers(string crewId, IEnumerable<Player> crewMembers)
    {
        crew?.Members.Clear();
        foreach (var crewMember in crewMembers)
        {
            Crew.Members.Add(crewMember);
        }

        return Task.CompletedTask;
    }

    public Task DeletePlayerHistory(string playerId)
    {
        DeletedPlayerId = playerId;

        return Task.CompletedTask;
    }

    public Task DeleteCrew(string crewId)
    {
        DeletedCrewId = crewId;

        return Task.CompletedTask;
    }

    public Task Disband(string crewId)
    {
        DisbandedCrewIds = [crewId];

        return Task.CompletedTask;
    }

    public Task Disband(string[] crewIds)
    {
        DisbandedCrewIds = crewIds;

        return Task.CompletedTask;
    }
}