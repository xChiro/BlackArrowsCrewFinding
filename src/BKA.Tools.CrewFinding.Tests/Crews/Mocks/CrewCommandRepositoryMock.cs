using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewCommandRepositoryMock : ICrewCommandRepository, ICrewDisbandRepository
{
    private readonly string _expectedCrewPartyId;

    public CrewCommandRepositoryMock(string expectedCrewPartyId = "123412")
    {
        _expectedCrewPartyId = expectedCrewPartyId;
    }

    public CrewName? Name { get; private set; }
    public Location? StartingPlace { get; private set; }
    public LanguageCollections? Languages { get; private set; }
    public Activity? Activity { get; private set; }
    public Player? Captain { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public IEnumerable<Player>? Members { get; private set; }
    public int MaxMembersAllowed { get; private set; }
    public bool Active { get; set; }
    public string DisbandedCrewId { get; private set; }

    public Task CreateCrew(Crew crew)
    {
        Name = crew.Name;
        StartingPlace = crew.ReunionPoint;
        Languages = crew.Languages;
        Activity = crew.Activity;
        Captain = crew.Captain;
        CreationDate = crew.CreatedAt;
        Members = crew.Members;
        MaxMembersAllowed = crew.Members.MaxSize;
        
        return Task.FromResult(_expectedCrewPartyId);
    }

    public Task UpdateMembers(string crewId, IEnumerable<Player> crewMembers)
    {
        Members = crewMembers;
        
        return Task.CompletedTask;
    }

    public Task Disband(string crewId)
    {
        DisbandedCrewId = crewId;
        
        return Task.CompletedTask;
    }
}