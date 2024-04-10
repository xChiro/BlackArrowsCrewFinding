using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class CrewPartyCommandsMock(string expectedCrewPartyId = "123412") : ICrewPartyCommands
{
    public CrewName? Name { get; private set; }
    public Location? StartingPlace { get; private set; }
    public LanguageCollections? Languages { get; private set; }
    public CrewCapacity? MaxCrewNumber { get; private set; }
    public Activity? Activity { get; private set; }
    public Player? Captain { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public IEnumerable<Player>? Members { get; private set; }

    public Task CreateCrewParty(CrewParty crewParty)
    {
        Name = crewParty.Name;
        StartingPlace = crewParty.ReunionPoint;
        Languages = crewParty.Languages;
        MaxCrewNumber = crewParty.CrewCapacity;
        Activity = crewParty.Activity;
        Captain = crewParty.Captain;
        CreationDate = crewParty.CreationAt;
        Members = crewParty.Members;
        
        return Task.FromResult(expectedCrewPartyId);
    }

    public Task UpdateMembers(string crewPartyId, IEnumerable<Player> crewPartyMembers)
    {
        Members = crewPartyMembers;
        
        return Task.CompletedTask;
    }
}