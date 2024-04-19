using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class CrewCommandsMock : ICrewCommands
{
    private readonly string _expectedCrewPartyId;

    public CrewCommandsMock(string expectedCrewPartyId = "123412")
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

    public Task CreateCrew(Crew crew)
    {
        Name = crew.Name;
        StartingPlace = crew.ReunionPoint;
        Languages = crew.Languages;
        Activity = crew.Activity;
        Captain = crew.Captain;
        CreationDate = crew.CreationAt;
        Members = crew.Members;
        
        return Task.FromResult(_expectedCrewPartyId);
    }

    public Task UpdateMembers(string crewPartyId, IEnumerable<Player> crewPartyMembers)
    {
        Members = crewPartyMembers;
        
        return Task.CompletedTask;
    }
}