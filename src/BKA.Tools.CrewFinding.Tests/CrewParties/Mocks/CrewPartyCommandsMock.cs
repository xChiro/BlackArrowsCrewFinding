using System;
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
    public string AddedPlayerId { private set; get; }

    public Task<string> SaveCrewParty(Player captain, CrewParty crewParty)
    {
        Name = crewParty.Name;
        StartingPlace = crewParty.ReunionPoint;
        Languages = crewParty.Languages;
        MaxCrewNumber = crewParty.TotalCrewCapacity;
        Activity = crewParty.Activity;
        Captain = captain;
        CreationDate = crewParty.CreationDate;
        
        return Task.FromResult(expectedCrewPartyId);
    }

    public Task AddPlayerToCrewParty(string playerId, string crewPartyId)
    {
        if (expectedCrewPartyId != crewPartyId)
            throw new Exception("Unexpected crew party id");
        
        AddedPlayerId = playerId;
        
        return Task.CompletedTask;
    }
}