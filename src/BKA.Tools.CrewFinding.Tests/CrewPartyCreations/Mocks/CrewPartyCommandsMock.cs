using System;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;

public class CrewPartyCommandsMock : ICrewPartyCommands
{
    public CrewName? Name { get; private set; }
    public Location? StartingPlace { get; private set; }
    public LanguageCollections? Languages { get; private set; }
    public CrewNumber? MaxCrewNumber { get; private set; }
    public Activity? Activity { get; private set; }
    public Player? Captain { get; private set; }
    public DateTime? CreationDate { get; private set; }

    public string SaveCrewParty(Player captain, CrewParty crewParty)
    {
        Name = crewParty.Name;
        StartingPlace = crewParty.ReunionPoint;
        Languages = crewParty.Languages;
        MaxCrewNumber = crewParty.TotalCrewNumber;
        Activity = crewParty.Activity;
        Captain = captain;
        CreationDate = crewParty.CreationDate;
        
        return Guid.NewGuid().ToString();
    }
}